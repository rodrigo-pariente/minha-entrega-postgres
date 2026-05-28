namespace MinhaEntrega.WebApp.Models;

public class Order(
    string code,
    string name,
    string status,
    DateOnly? deliveryDate = null,
    List<Event>? Events = null
)
{
    public string Code { get; set; } = code;

    public string Name { get; set; } = name;

    public string Status { get; set; } = status;

    public DateOnly? DeliveryDate { get; set; } = deliveryDate;

    public List<Event> Events { get; set; } = Events ?? []; 
}
