"""Update orders information in the database"""

from core.die import die
from ui.panels import print_panel
from api_requests import api_requests

def update_order(name: str, new_name: str) -> None:
    if len(new_name) > 32:
        die("O nome de um rastreio deve ter entre 1 e 32 caracteres")

    order, success = api_requests.get_order_by_name(name)
    if not success:
        die("Não foi possível visualizar {name}")

    if not api_requests.put_order(order.code, new_name):
        die(f"Não foi possivel atualizar {name}")

    print_panel(f"{name} → {new_name}", "Atualizado")
