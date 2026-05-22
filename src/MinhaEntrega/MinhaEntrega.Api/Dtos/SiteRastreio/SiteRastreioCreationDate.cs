using System.Text.Json.Serialization;

namespace MinhaEntrega.Api.Dtos.SiteRastreio;

record SiteRastreioCreationDate(
    [property: JsonPropertyName("date")] string Datetime
);
