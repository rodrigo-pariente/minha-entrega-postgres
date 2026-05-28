namespace MinhaEntrega.WebApp.Models;

public static class OrderStatus
{
    public static IReadOnlyList<string> OrderStatusStrings =
    [
        "❓ Desconhecido",
        "✔️ Entregue",
        "🚚 Trânsito",
        "🛂 Análise",
        "❓ Perdido",
        "📌 Criado",
        "📦 Entrega",
        "📥 Devolvido",
        "🏤 Retirada"
    ];
}
