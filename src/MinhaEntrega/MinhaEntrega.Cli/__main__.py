"""Frontend para a API Minha Entrega."""

import typer
from typing import Annotated

import actions

app = typer.Typer(no_args_is_help=True)

@app.command("list")
def list_command(
    names: Annotated[list[str] | None, typer.Argument()] = None
) -> None:
    """Mostra um breve sumário das entregas."""
    actions.list_orders(names)

@app.command("add")
def add_command(
    code: Annotated[str, typer.Argument()],
    name: Annotated[str | None, typer.Argument()] = None
) -> None:
    """Adiciona uma entrega."""
    actions.add_order(code, name)

@app.command("rm")
@app.command("remove")
def remove_command(names: Annotated[list[str], typer.Argument()]) -> None:
    """Remove uma entrega."""
    actions.remove_orders(names)

@app.command()
def refresh(
    names: Annotated[list[str], typer.Argument()] = None,
    dumb: bool = False
) -> None:
    """Atualiza as informações de uma entrega."""
    actions.refresh_orders(names, dumb)

@app.command()
def update(
    name: Annotated[str, typer.Argument()],
    new_name: Annotated[str, typer.Argument()]
) -> None:
    """Atualiza informações estáticas de uma entrega."""
    actions.update_order(name, new_name)

@app.command()
def events(name: str) -> None:
    """Mostra todos os eventos registrados em uma entrega."""
    actions.list_order_events(name)

@app.command()
def inspect(
    names: Annotated[list[str] | None, typer.Argument()] = None
) -> None:
    """Mostra as informações mais recentes de uma entrega."""
    actions.inspect_orders(names)


if __name__ == "__main__":
    app()
