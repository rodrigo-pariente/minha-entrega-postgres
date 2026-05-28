using MinhaEntrega.Api.Models;
using MinhaEntrega.Api.Dtos;

namespace MinhaEntrega.Api.Core;

static class DtoConvert
{
    public static EventDto EventToEventDto(Event orderEvent)
    {
        return new EventDto(
            orderEvent.Origin,
            orderEvent.Destination,
            orderEvent.Description,
            orderEvent.Date
        );
    }

    public static OrderDto OrderToOrderDto(Order order)
    {
        return new OrderDto(
            order.Code,
            order.Name,
            [.. order.Events.OrderBy(e => e.Date).Select(EventToEventDto)], // spread operator
            order.Status,
            order.DeliveryDate
        );
    }

    // public static Event EventDtoToEvent(EventDto orderEventDto)
    // {
    //     return new Event(
    //         orderEventDto.Origin,
    //         orderEventDto.Destination,
    //         orderEventDto.Description,
    //         orderEventDto.Date
    //     );
    // }
    //
    // public static OrderDetails OrderDtoToOrderDetails(OrderDto orderDto)
    // {
    //     return new OrderDetails(
    //         orderDto.Code,
    //         orderDto.Name,
    //         [.. orderDto.Events.Select(EventDtoToEvent)],
    //         orderDto.Status,
    //         orderDto.DeliveryDate
    //     );
    // }
}
