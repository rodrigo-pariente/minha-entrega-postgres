"""Add order to the database"""

import re
from ui.panels import print_panel
from core.die import die
from api_requests import api_requests

def is_valid_correios_code(code: str) -> bool:
    return re.search("[A-Z]{2}[0-9]{9}[A-Z]{2}", code)

def add_order(code: str, name: str | None) -> None:
    if not is_valid_correios_code(code):
        die("Código de rastreio inválido")

    if name is None:
        name = code

    if  len(name) > 32:
        die("O nome de um rastreio deve ter entre 1 e 32 caracteres")

    if not api_requests.post_order(code, name):
        die("Não foi possível adicionar rastreio")

    lines  = f"[dim]{'name':16} code[/dim]\n"
    lines += f"{name:16} {code} 📦"

    print_panel(lines, "Adicionado")
