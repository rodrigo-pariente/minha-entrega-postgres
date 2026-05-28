using MinhaEntrega.WebApp.Client;
using MinhaEntrega.WebApp.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

DebuggingUtils.WriteLine("GetOrderAsync", await MinhaEntregaClient.GetOrdersAsync());
app.Run();
