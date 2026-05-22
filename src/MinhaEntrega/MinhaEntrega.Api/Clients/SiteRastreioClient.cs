using MinhaEntrega.Api.Utils;

using MinhaEntrega.Api.Core;
using MinhaEntrega.Api.Models;
using MinhaEntrega.Api.Dtos.SiteRastreio;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MinhaEntrega.Api.Clients;

public class SiteRastreioClient(string apiKey)
{
    public string ApiKey { get; set; } = apiKey;

    private const string URL =
        "https://api-labs.wonca.com.br/wonca.labs.v1.LabsService/Track";

    private async Task<SiteRastreioOrder?> RequestAsync(string trackingCode)
    {
        using var client = new HttpClient();

        // Headers
        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
        );

        client.DefaultRequestHeaders.Add("Authorization", $"Apikey {ApiKey}");

        // Response
        var response = await client.PostAsJsonAsync(URL, new { code = trackingCode });

        if (!response.IsSuccessStatusCode)
        {
            throw new Exceptions.SiteRastreioFailedRequestException(trackingCode);
        }

        var readableResponse = await response.Content.ReadAsStringAsync();

        DebuggingUtils.WriteLine("SiteRastreioResponse", readableResponse);

        var json = response.Content.ReadFromJsonAsync<SiteRastreioJson>().Result;
        return JsonSerializer.Deserialize<SiteRastreioOrder>(json!.Json);
    }

    private static string SiteRastreioUnitToString(SiteRastreioUnit unit)
    {
        IReadOnlyList<string?> properties = [
            unit.Name,
            unit.Type,
            unit.Address.Main,
            unit.Address.Number,
            unit.Address.Place,
            unit.Address.Details,
            unit.Address.Block,
            unit.Address.City,
            unit.Address.State,
            unit.Address.ZipCode,
            unit.Address.CountryAbbreviation
        ];
        return string.Join(" - ", properties.Where(p => !string.IsNullOrWhiteSpace(p)));
    }

    private static Event SiteRastreioEventToEvent(SiteRastreioEvent sREvent)
    {
        string origin = sREvent.Unit == null ? string.Empty :
            SiteRastreioUnitToString(sREvent.Unit);

        string dest = sREvent.DestinationUnit == null ? string.Empty :
            SiteRastreioUnitToString(sREvent.DestinationUnit);

        IReadOnlyList<string?> descriptionParts = [
            sREvent.Description,
            sREvent.Comment,
            sREvent.Details
        ];
        string desc = string.Join(" - ", descriptionParts.
            Where(p => !string.IsNullOrWhiteSpace(p)));

        var success = DateTime.TryParse(sREvent.CreationDate.Datetime, out DateTime dt);

        DateTime? datetime = success ? DateTime.SpecifyKind(dt, DateTimeKind.Utc) : null;

        return new Event(origin, dest, desc, datetime);
    }

    private static OrderDetails SiteRastreioOrderDtoToOrderDetails(
        SiteRastreioOrder sROrder,
        string code,
        string name
    )
    {
        List<Event> events = [];
        foreach (var siteRastreioEvent in sROrder.Events)
        {
            events.Add(SiteRastreioEventToEvent(siteRastreioEvent));
        }

        OrderStatus status;
        if (sROrder.HasDeliveryEvent)
        {
            status = OrderStatus.Deliver;
        }
        else if (sROrder.Status.Equals("E"))
        {
            status = OrderStatus.Delivered;
        }
        else if(sROrder.Status.Equals("T"))
        {
            status = OrderStatus.Transit;
        }
        else
        {
            status = OrderStatus.Unknown;
        }

        var date = DateOnly.ParseExact(
            sROrder.ExpectedDate,
            "dd/MM/yyyy",
            System.Globalization.CultureInfo.InvariantCulture
        );

        return new OrderDetails(code, name, events, status, date);
    }

    public async Task<OrderDetails> GetOrderDetailsAsync(string code, string? name = null)
    {
        name ??= code;

        var order = await RequestAsync(code);

        if (order is null)
        {
            return new OrderDetails(code, name, [], OrderStatus.Unknown, null);
        }

        Console.WriteLine($"[DEBUG] order: {order}");
        return SiteRastreioOrderDtoToOrderDetails(order, code, name);
    }
}
