"""Print small summary of the database orders"""

from ui.panels import print_panel, print_error_panel
from api_requests import api_requests

def get_order_string(order) -> str:
    return f'\n{order.name:16} {order.code} ({order.status})'

def list_orders(names: list[str] | None) -> None:
    lines = f"[dim]{"name":16} {"code":13} (status)[/dim]"
    if names is None:
        for order in api_requests.get_orders():
            lines += get_order_string(order)
    else:
        failed = set()
        for name in names:
            order, success = api_requests.get_order_by_name(name)
            if not success:
                failed.add(name)
                continue
            lines += get_order_string(order)

        if len(failed) != 0:
            content = "Não foi possível visualizar\n\n" + "\n".join(failed)
            print_error_panel(content)

    print_panel(lines, "Rastreios")
