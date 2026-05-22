using System.Text.Json.Serialization;

namespace MinhaEntrega.Api.Dtos.SiteRastreio;

record SiteRastreioOrder(
    [property: JsonPropertyName("situacao")] string Status,

    [property: JsonPropertyName("dtPrevista")] string ExpectedDate,

    [property: JsonPropertyName("eventos")] List<SiteRastreioEvent> Events,

    [property: JsonPropertyName("temEventoEntrega")] bool HasDeliveryEvent
);
