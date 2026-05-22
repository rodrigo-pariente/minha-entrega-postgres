using System.Text.Json.Serialization;

namespace MinhaEntrega.Api.Dtos.SiteRastreio;

record SiteRastreioJson(
    [property: JsonPropertyName("json")] string Json
);
