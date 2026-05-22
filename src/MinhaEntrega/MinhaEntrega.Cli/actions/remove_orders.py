"""Remove orders from the database"""

from ui.panels import print_error_panel, print_panel
from api_requests import api_requests

def cross_string(string: str) -> str:
    result = ""
    for c in string:
        result += c + "\u0336"
    return result

def remove_orders(names: list[str]) -> None:
    lines = f"[dim]{'name':16} code[/dim]\n"
    for name in names:
        failed = set()
        for name in names:
            order, success = api_requests.get_order_by_name(name)
            if success and api_requests.delete_order(order.code):
                lines += cross_string(f'{name:16} {order.code}')
            else:
                failed.add(name)

        if len(failed) != 0:
            content = "Não foi possível remover:\n\n" + "\n".join(failed)
            print_error_panel(content)

        print_panel(lines, "Removido")
