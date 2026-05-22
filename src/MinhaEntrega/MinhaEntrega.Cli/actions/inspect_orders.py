"""Inspect orders last events"""

from rich import print
from rich.console import Group
from rich.panel import Panel
from ui.panels import get_panel, print_error_panel
from ui.title import print_title
from ui import to_string
from api_requests import api_requests
from models.order import Order

def get_inspection_panel(order: Order) -> Panel:
    title = f"[bold green]{order.name} - {order.code}"

    if len(order.events) == 0:
        return get_panel("", title)

    content  = f"[dim]{order.status}[/dim]\n"
    content += f"[dim]Previsão de Entrega: {order.delivery_date}[/dim]\n\n"
    content += to_string.event_to_string(order.events[-1])

    return get_panel(content, title)

def inspect_orders(names: str | None) -> None:
    panels = set()
    failed = set()
    if names is None:
        for order in api_requests.get_orders():
            panels.add(get_inspection_panel(order))
    else:
        for name in names:
            order, success = api_requests.get_order_by_name(name)
            if not success:
                failed.add(name)
            else:
                panels.add(get_inspection_panel(order))

        if len(failed) != 0:
            content = "Rastreios desconhecidos:\n\n" + "\n".join(failed)
            print_error_panel(content)

    if len(panels) != 0:
        print_title("Inspeção")
        print(Group(*panels))
