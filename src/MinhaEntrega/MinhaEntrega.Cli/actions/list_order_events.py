"""List orders in the database"""

from ui import to_string
from rich import print
from rich.console import Group
from core.die import die
from ui.panels import get_panel
from ui.title import print_title
from api_requests import api_requests

def list_order_events(name: str) -> None:
    order, success = api_requests.get_order_by_name(name)
    if not success:
        die(f"Não foi possivel visualizar {name}")

    panels = set()
    for event in order.events:
        event_desc = to_string.event_to_string(event)
        event_panel = get_panel(event_desc, event.date)
        panels.add(event_panel)

    print_title("Eventos", name)
    if len(panels) != 0:
        print(Group(*panels))
