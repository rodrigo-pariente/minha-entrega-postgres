namespace MinhaEntrega.Api.Dtos;

record EventDto(
    string? Origin,
    string? Destination,
    string? Description,
    DateTime? Date
);
