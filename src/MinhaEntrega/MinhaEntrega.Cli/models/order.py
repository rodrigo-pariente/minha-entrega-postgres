from dataclasses import dataclass
from models.event import Event

@dataclass
class Order:
    """Modeled after MinhaEntrega Order"""
    code: str
    name: str
    status: str
    delivery_date: str
    events: list[Event]
