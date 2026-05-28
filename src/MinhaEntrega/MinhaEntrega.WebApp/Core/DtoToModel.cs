using MinhaEntrega.WebApp.Models;
using MinhaEntrega.WebApp.Dtos;

namespace MinhaEntrega.WebApp.Core;

public static class DtoToModel
{
    public static Event EventDtoToEvent(EventDto eventDto)
    {
        return new Event(
            eventDto.Origin,
            eventDto.Destination,
            eventDto.Description,
            eventDto.Date
        );
    }

    private static string GetOrderStatus(int statusNumber)
    {
        return OrderStatus.OrderStatusStrings[statusNumber];
    }

    public static Order OrderDtoToOrder(OrderDto orderDto)
    {
        return new Order(
            orderDto.Code,
            orderDto.Name,
            GetOrderStatus(orderDto.Status),
            orderDto.DeliveryDate,
            [.. orderDto.Events.Select(EventDtoToEvent)]
        );
    }
}
