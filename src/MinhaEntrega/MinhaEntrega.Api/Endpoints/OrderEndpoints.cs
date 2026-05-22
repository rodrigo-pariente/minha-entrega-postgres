using MinhaEntrega.Api.Clients;
using MinhaEntrega.Api.Data;
using MinhaEntrega.Api.Dtos;
using MinhaEntrega.Api.Models;
using MinhaEntrega.Api.Core;
using Microsoft.EntityFrameworkCore;

using System.Text.RegularExpressions;

namespace MinhaEntrega.Api.Endpoints;

public static class OrderEndpoints
{
    const string GetOrderEndpointName = "GetOrder";

    public static void MapOrdersEndpoints(this WebApplication app, SiteRastreioClient siteRastreioClient)
    {
        var group = app.MapGroup("/orders");

        // GET /orders
        group.MapGet("/", (MinhaEntregaContext dbContext) =>
            dbContext.Orders
                .AsNoTracking()
                .Include(order => order.Events)
                .Select(DtoConvert.OrderToOrderDto)
        );

        // GET /orders/{code}
        group.MapGet("/{code}", async (string code, MinhaEntregaContext dbContext) =>
        {
            var order = await dbContext.Orders
                .AsNoTracking()
                .Include(order => order.Events)
                .FirstOrDefaultAsync(order => order.Code == code);

            return order is null ? Results.NotFound() :
                Results.Ok(DtoConvert.OrderToOrderDto(order));
        }).WithName(GetOrderEndpointName);

        // GET /orders/name/{name}
        group.MapGet("/name/{name}", async (string name, MinhaEntregaContext dbContext) =>
        {
            var order = await dbContext.Orders
                .AsNoTracking()
                .Include(order => order.Events)
                .FirstOrDefaultAsync(order => order.Name == name);

            return order is null ? Results.NotFound() :
                Results.Ok(DtoConvert.OrderToOrderDto(order));
        });

        // GET /orders/inspect
        group.MapGet("/inspect", (MinhaEntregaContext dbContext) =>
            dbContext.Orders.AsNoTracking().Select(order =>
                DtoConvert.EventToEventDto(order.Events.OrderBy(e => e.Date).Last()
        )));

        // GET /orders/{code}/inspect
        group.MapGet("/{code}/inspect", async (string code, MinhaEntregaContext dbContext) =>
        {
            var order = await dbContext.Orders
                .AsNoTracking()
                .Include(order => order.Events)
                .FirstOrDefaultAsync(order => order.Code == code);

            if (order is null)
            {
                return Results.NotFound();
            }

            if (order.Events.Count == 0)
            {
                return Results.NoContent();
            }

            var lastEvent = order.Events.OrderBy(e => e.Date).Last();

            return Results.Ok(DtoConvert.EventToEventDto(lastEvent));
        });

        // POST /orders
        group.MapPost("/", async (CreateOrderDto newOrder, MinhaEntregaContext dbContext) =>
        {
            var regex = new Regex(@"^[A-Z]{2}[0-9]{9}[A-Z]{2}$");

            if (!regex.IsMatch(newOrder.Code))
            {
                return Results.BadRequest();
            }

            var orderDto = new OrderDto(
                newOrder.Code,
                newOrder.Name ?? newOrder.Code,
                [],
                OrderStatus.Unknown,
                null
            );

            if (dbContext.Orders.FirstOrDefault(order => order.Name.Equals(orderDto.Name)) is not null)
            {
                return Results.BadRequest();
            }

            var order = new Order(
                orderDto.Code,
                orderDto.Name,
                orderDto.Status,
                orderDto.DeliveryDate
            );

            await dbContext.Orders.AddAsync(order);

            try
            {
                dbContext.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return Results.BadRequest(Exceptions.MinhaEntregaInvalidNameException.DefaultMessage);
            }

            return Results.CreatedAtRoute(
                GetOrderEndpointName,
                new { code = orderDto.Code },
                orderDto
            );
        });

        // POST /orders/refresh
        group.MapPost("/refresh", async (MinhaEntregaContext dbContext) =>
        {
            await dbContext.SmartRefreshAllOrdersAsync(siteRastreioClient);

            dbContext.SaveChanges();

            IReadOnlyList<OrderDto> orderDtos = [.. dbContext.Orders.Select(DtoConvert.OrderToOrderDto)];

            return Results.Ok(orderDtos);
        });

        // POST /orders/dumb_refresh
        group.MapPost("/dumb_refresh", async (MinhaEntregaContext dbContext) =>
        {
            await dbContext.RefreshAllOrdersAsync(siteRastreioClient);

            dbContext.SaveChanges();

            IReadOnlyList<OrderDto> orderDtos = [.. dbContext.Orders.Select(DtoConvert.OrderToOrderDto)];

            return Results.Ok(orderDtos);
        });

        // POST /orders/{code}/refresh
        group.MapPost("/{trackingCode}/refresh", async (string trackingCode, MinhaEntregaContext dbContext) =>
        {
            var order = dbContext.Orders.Find(trackingCode);

            if (order is null)
            {
                return Results.NotFound();
            }

            try
            {
                await dbContext.RefreshOrderAsync(siteRastreioClient, trackingCode);
            }
            catch(Exceptions.SiteRastreioFailedRequestException ex)
            {
                return Results.InternalServerError(ex.Message);
            }

            dbContext.SaveChanges();

            return Results.Ok(DtoConvert.OrderToOrderDto(order));
        });

        // PUT /orders/{code}
        group.MapPut("/{code}", async (string code, UpdateOrderDto updateOrder, MinhaEntregaContext dbContext) =>
        {
            var order = await dbContext.Orders.FindAsync(code);

            if (order is null)
            {
                return Results.NotFound();
            }

            order.Name = updateOrder.Name ?? order.Name;

            try
            {
                dbContext.SaveChanges();
            }
            catch(DbUpdateException)
            {
                return Results.BadRequest(Exceptions.MinhaEntregaInvalidNameException.DefaultMessage);
            }

            return Results.NoContent();
        });

        // DELETE /orders/{code}
        group.MapDelete("/{code}", async (string code, MinhaEntregaContext dbContext) =>
        {
            await dbContext.Orders.Where(order => order.Code == code).ExecuteDeleteAsync();

            return Results.NoContent();
        });
    }
}
