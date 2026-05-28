namespace MinhaEntrega.WebApp.Models;

public class Event(
    string origin = "",
    string destination = "",
    string description = "",
    DateTime? date = null
)
{
    public string Origin { get; set; } = origin;

    public string Destination { get; set; } = destination;

    public string Description { get; set; } = description;

    public DateTime? Date { get; set; } = date;
}
