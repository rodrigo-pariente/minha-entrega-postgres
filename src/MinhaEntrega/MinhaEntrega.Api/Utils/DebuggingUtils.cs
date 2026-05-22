namespace MinhaEntrega.Api.Utils;

public static class DebuggingUtils
{
    public static void WriteLine(string s, object obj)
    {
        Console.Error.WriteLine($"[DEBUG] {s}: {obj}");
    }
}
