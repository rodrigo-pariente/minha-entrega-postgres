"""Methods to convert from MinhaEntrega.Api Dtos to python data"""

from models.event import Event
from models.order import Order
from models.order_status import order_status

def event_dto_to_event(event_dto: dict) -> Order:
    """Convert from an event_dto dictionary to Order"""
    return Event(
        event_dto["origin"],
        event_dto["destination"],
        event_dto["description"],
        event_dto["date"]
    )

def order_dto_to_order(order_dto: dict) -> Order:
    """Convert from an order_dto dictionary to Order"""
    return Order(
        order_dto["code"],
        order_dto["name"],
        order_status[order_dto["status"]],
        order_dto["deliveryDate"],
        list(map(event_dto_to_event, order_dto["events"]))
    )
