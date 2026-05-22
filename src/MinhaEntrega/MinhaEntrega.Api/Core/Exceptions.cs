namespace MinhaEntrega.Api.Core;

public class Exceptions
{
    public class MinhaEntregaException : Exception
    {
        public MinhaEntregaException() {}

        public MinhaEntregaException(string message)
            : base (message) {}
    }

    public class MinhaEntregaInvalidNameException : MinhaEntregaException
    {
        public static string DefaultMessage { get; } = "Name already exists or is too long.";

        public MinhaEntregaInvalidNameException()
            : base("Name already exists or is too long.") {}

        public MinhaEntregaInvalidNameException(string name)
            : base($"Name {name} already exists or is too long.") {}
    }

    public class SiteRastreioFailedRequestException : MinhaEntregaException
    {
        public SiteRastreioFailedRequestException() {}

        public SiteRastreioFailedRequestException(string code)
            : base($"Failed request to SiteRastreio for \"{code}\" order.") {}
    }
}
