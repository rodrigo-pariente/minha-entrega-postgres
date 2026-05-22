using MinhaEntrega.Api.Models;

namespace MinhaEntrega.Api.Dtos;

record OrderDto(
    string Code,
    string Name,
    List<EventDto> Events,
    OrderStatus Status,
    DateOnly? DeliveryDate
);
