from dataclasses import dataclass

@dataclass
class Event:
    """Modeled after MinhaEntrega Event"""
    origin: str | None
    destination: str | None
    description: str | None
    date: str | None
