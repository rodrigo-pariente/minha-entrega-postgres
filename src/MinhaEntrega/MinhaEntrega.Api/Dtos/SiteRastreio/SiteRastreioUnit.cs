using System.Text.Json.Serialization;

namespace MinhaEntrega.Api.Dtos.SiteRastreio;

record SiteRastreioUnit(
    [property: JsonPropertyName("nome")] string? Name,

    [property: JsonPropertyName("tipo")] string? Type,

    [property: JsonPropertyName("endereco")] SiteRastreioAddress Address
);
