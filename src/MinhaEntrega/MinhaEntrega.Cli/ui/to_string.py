"""Methods to turn data models into string"""

from models.event import Event

def event_to_string(event: Event) -> str:
    content  = f"[bold cyan]  De: [/bold cyan]{event.origin}\n"
    content += f"[bold cyan]Para: [/bold cyan]{event.destination}\n"
    content += f"[bold cyan]Desc: [/bold cyan]{event.description}"
    return content
