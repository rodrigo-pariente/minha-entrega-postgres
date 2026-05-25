using System.Text.Json.Serialization;

namespace MinhaEntrega.Api.Dtos.SiteRastreio;

record SiteRastreioEvent(
    [property: JsonPropertyName("dtHrCriado")] SiteRastreioCreationDate CreationDate,

    [property: JsonPropertyName("descricao")] string? Description,

    [property: JsonPropertyName("comentario")] string? Comment,

    [property: JsonPropertyName("detalhe")] string? Details,

    [property: JsonPropertyName("unidade")] SiteRastreioUnit? Unit,

    [property: JsonPropertyName("unidadeDestino")] SiteRastreioUnit? DestinationUnit,

    [property: JsonPropertyName("rota")] string? Route,

    [property: JsonPropertyName("descricaoWeb")] string? WebDescription
);
