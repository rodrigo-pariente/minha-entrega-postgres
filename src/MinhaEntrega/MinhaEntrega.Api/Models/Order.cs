using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MinhaEntrega.Api.Models;

[Index(nameof(Name), IsUnique = true)]
public class Order(
    string code,
    string name,
    OrderStatus status = OrderStatus.Unknown,
    DateOnly? deliveryDate = null
)
{
    [Key]
    public string Code { get; set; } = code;

    public string Name { get; set; } = name;

    public OrderStatus Status { get; set; } = status;

    public DateOnly? DeliveryDate { get; set; } = deliveryDate;

    public List<Event> Events { get; set; } = [];
}
