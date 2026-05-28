namespace MinhaEntrega.WebApp.Dtos;

public class OrderDto(
    string code,
    string name,
    int status,
    DateOnly? deliveryDate = null
)
{
    public string Code { get; set; } = code;

    public string Name { get; set; } = name;

    public int Status { get; set; } = status;

    public DateOnly? DeliveryDate { get; set; } = deliveryDate;

    public List<EventDto> Events { get; set; } = [];
}
