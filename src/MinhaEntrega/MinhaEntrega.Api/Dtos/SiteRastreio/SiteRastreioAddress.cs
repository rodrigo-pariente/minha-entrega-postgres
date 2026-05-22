using System.Text.Json.Serialization;

namespace MinhaEntrega.Api.Dtos.SiteRastreio;

record SiteRastreioAddress(
    [property: JsonPropertyName("identificacao")] string? Id,

    [property: JsonPropertyName("principal")] string? Main,

    [property: JsonPropertyName("numero")] string? Number,

    [property: JsonPropertyName("logradouro")] string? Place,

    [property: JsonPropertyName("complemento")] string? Details,

    [property: JsonPropertyName("bairro")] string? Block,

    [property: JsonPropertyName("cidade")] string? City,

    [property: JsonPropertyName("uf")] string? State,

    [property: JsonPropertyName("codigoPostal")] string? ZipCode,

    [property: JsonPropertyName("siglaPais")] string? CountryAbbreviation
);
