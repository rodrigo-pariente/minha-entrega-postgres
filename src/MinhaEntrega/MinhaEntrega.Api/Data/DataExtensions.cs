using MinhaEntrega.Api.Core;
using MinhaEntrega.Api.Models;
using MinhaEntrega.Api.Clients;
using Microsoft.EntityFrameworkCore;

namespace MinhaEntrega.Api.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<MinhaEntregaContext>();

        dbContext.Database.Migrate();
    }

    public static void AddMinhaEntregaDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.
            GetConnectionString("DefaultConnection");

        builder.Services.AddDbContextPool<MinhaEntregaContext>(opt =>
            opt.UseNpgsql(connString)
        );
    }

    public static async Task RefreshOrderAsync(
        this MinhaEntregaContext dbContext,
        SiteRastreioClient siteRastreioClient,
        string code
    )
    {
        var order = await dbContext.Orders
            .Include(o => o.Events)
            .FirstOrDefaultAsync(o => o.Code == code);

        if (order is null)
        {
            return;
        }

        var updated = await siteRastreioClient
            .GetOrderDetailsAsync(order.Code, order.Name);

        dbContext.Events.RemoveRange(order.Events);

        await dbContext.SaveChangesAsync();

        order.Events.Clear();

        foreach (var ev in updated.Events)
        {
            order.Events.Add(ev);
        }

        order.Status = updated.Status;
        order.DeliveryDate = updated.DeliveryDate;

        await dbContext.SaveChangesAsync();
    }

    public static async Task SmartRefreshAllOrdersAsync(
        this MinhaEntregaContext dbContext,
        SiteRastreioClient siteRastreioClient
    )
    {
        var codes = await dbContext.Orders
            .Select(o => o.Code)
            .ToListAsync();

        foreach (var code in codes)
        {
            try
            {
                var order = await dbContext.Orders.FindAsync(code);
                if (order!.Status == OrderStatus.Delivered)
                {
                    continue;
                }

                await dbContext.RefreshOrderAsync(
                    siteRastreioClient,
                    code
                );
            }
            catch (Exceptions.SiteRastreioFailedRequestException)
            {
                continue;
            }
        }
    }

    public static async Task RefreshAllOrdersAsync(
        this MinhaEntregaContext dbContext,
        SiteRastreioClient siteRastreioClient
    )
    {
        var codes = await dbContext.Orders
            .Select(o => o.Code)
            .ToListAsync();

        foreach (var code in codes)
        {
            try
            {
                await dbContext.RefreshOrderAsync(
                    siteRastreioClient,
                    code
                );
            }
            catch (Exceptions.SiteRastreioFailedRequestException)
            {
                continue;
            }
        }
    }
}
