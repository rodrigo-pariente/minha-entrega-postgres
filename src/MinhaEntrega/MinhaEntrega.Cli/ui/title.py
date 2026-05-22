"""Custom title to main outputs"""

from rich import print

def print_title(title: str, subtitle: str = "") -> None:
    print()
    if subtitle:
        subtitle = f": {subtitle}"
    print(f"[bold yellow]{title}[/bold yellow]{subtitle}")
    print()
