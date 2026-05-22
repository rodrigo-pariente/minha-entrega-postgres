using System.ComponentModel.DataAnnotations;

namespace MinhaEntrega.Api.Dtos;

record UpdateOrderDto(
    [Required][StringLength(32)] string Name
);
