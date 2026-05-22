using System.ComponentModel.DataAnnotations;

namespace MinhaEntrega.Api.Models;

public class OrderDetails(
    string code,
    string name,
    List<Event>? events,
    OrderStatus status = OrderStatus.Unknown,
    DateOnly? deliveryDate = null
)
{
    [Key]
    public string Code { get; set; } = code;

    public string Name { get; set; } = name;

    public OrderStatus Status { get; set; } = status;

    public DateOnly? DeliveryDate { get; set; } = deliveryDate;

    public List<Event> Events { get; set; } = events ?? [];
}
