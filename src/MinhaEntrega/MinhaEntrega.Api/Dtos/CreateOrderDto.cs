using System.ComponentModel.DataAnnotations;

namespace MinhaEntrega.Api.Dtos;

record CreateOrderDto(
    [Required][RegularExpression(@"^[A-Z]{2}[0-9]{9}[A-Z]{2}$")] string Code,
    [StringLength(32)] string Name
);
