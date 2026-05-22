"""Custom panels for the cli output"""

import sys
from rich import print
from rich.panel import Panel
from rich.style import Style

panel_style = Style(color="green", bold=True)
error_panel_style = Style(color="red", bold=True)

def print_error_panel(content) -> None:
    panel = Panel(
        content,
        title="Error",
        border_style=error_panel_style,
        title_align="left",
        width=80
    )
    print(panel, file=sys.stderr)

def get_panel(content, title: str) -> None:
    return Panel(
        content,
        title=title,
        border_style=panel_style,
        title_align="left",
        width=80
    )

def print_panel(content, title: str) -> None:
    print(get_panel(content, title))
