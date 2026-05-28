using MinhaEntrega.WebApp.Models;
using MinhaEntrega.WebApp.Dtos;
using MinhaEntrega.WebApp.Core;
using MinhaEntrega.WebApp.Utils;

namespace MinhaEntrega.WebApp.Client;

public static class MinhaEntregaClient
{
    public static readonly Uri URL = new("http://localhost:5089/orders/");

    private static readonly HttpClient client = new();

    public static async Task<IReadOnlyList<Order>?> GetOrdersAsync()
    {
        var response = await client.GetAsync(URL);
        DebuggingUtils.WriteLine("content", await response.Content.ReadAsStringAsync());
        IReadOnlyList<OrderDto>? ordersDto = response.Content.ReadFromJsonAsync<IReadOnlyList<OrderDto>>().Result;
        if (ordersDto is null)
        {
            return null;
        }
        return [.. ordersDto.Select(DtoToModel.OrderDtoToOrder)];
    }

    public static async Task RemoveOrderAsync(string code)
    {
        await client.DeleteAsync(new Uri(URL, code));
    }

    public static async Task UpdateOrderAsync(string code, string orderName)
    {
        var url = new Uri(URL, code);
        var response = await client.PutAsJsonAsync(url, new { name = orderName });

        DebuggingUtils.WriteLine("update", await response.Content.ReadAsStringAsync());
        DebuggingUtils.WriteLine("update success", response.IsSuccessStatusCode);
        DebuggingUtils.WriteLine("url", url);
    }

    public static async Task AddOrderAsync(string orderCode, string orderName)
    {
        var response = await client.PostAsJsonAsync(URL, new { code = orderCode, name = orderName });

        DebuggingUtils.WriteLine("add", await response.Content.ReadAsStringAsync());
        DebuggingUtils.WriteLine("add success", response.IsSuccessStatusCode);
        DebuggingUtils.WriteLine("url", URL);
    }

    public static async Task<Event?> InspectOrderAsync(string code)
    {
        var response = await client.GetAsync(new Uri(URL, $"{code}/inspect"));
        return response.Content.ReadFromJsonAsync<Event>().Result;
    }

    public static async Task<IReadOnlyList<Order>?> GetOrderEventsAsycn(string code)
    {
        var response = await client.GetAsync(new Uri(URL, code));
        var orders = response.Content.ReadFromJsonAsync<IReadOnlyList<Order>>().Result;
        return orders;
    }
}
