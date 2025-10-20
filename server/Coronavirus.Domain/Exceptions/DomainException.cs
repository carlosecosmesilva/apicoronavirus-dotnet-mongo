namespace Coronavirus.Domain.Exceptions;

/// <summary>
/// Exceção customizada para erros de domínio/negócio.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
