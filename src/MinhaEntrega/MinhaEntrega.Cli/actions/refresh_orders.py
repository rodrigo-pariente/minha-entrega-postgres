"""Send refresh calls to the API"""

from core.die import die
from ui.panels import print_panel, print_error_panel
from models.order import Order
from api_requests import api_requests

def get_refreshed_order_string(order: Order) -> str:
    return f"{order.name:16} {order.code}\n"

def refresh_orders(names: list[str], dumb: bool) -> None:
    lines = f"[dim]{"name":16} code[/dim]\n"
    if names is None:
        if dumb:
            orders, success = api_requests.dumb_refresh_all_orders()
        else:
            orders, success = api_requests.refresh_all_orders()

        if not success:
            die("Não foi possível atualizar")

        for order in orders:
            lines += get_refreshed_order_string(order)
    else:
        failed = set()
        for name in names:
            order, success = api_requests.get_order_by_name(name)
            if not success:
                failed.add(name)
                continue
            if not api_requests.refresh_order(order.code):
                failed.add()
            else:
                lines += get_refreshed_order_string(order)

        if len(failed) != 0:
            content = "Não foi possível atualizar:\n\n" + "\n".join(failed)
            print_error_panel(content)

    print_panel(lines[0:-1], "Atualizado")
