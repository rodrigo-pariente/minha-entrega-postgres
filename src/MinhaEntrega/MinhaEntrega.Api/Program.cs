// Minha Entrega API
//   - API para gerenciar um banco de dados PostgreSQL
//     com suas entregas dos correios.
// rodrigo-pariente
//   - github: github.com/rodrigo-pariente

using MinhaEntrega.Api.Clients;
using MinhaEntrega.Api.Endpoints;
using MinhaEntrega.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("WebApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5208")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.AddMinhaEntregaDb();

var app = builder.Build();

app.UseCors("WebApp");

var siteRastreioClient = new SiteRastreioClient(builder.Configuration["SiteRastreioClient:ApiKey"]!);

app.MapOrdersEndpoints(siteRastreioClient);

app.MigrateDb();

app.Run();
