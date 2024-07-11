using System.Net;

namespace Kfoodle.Core.Exceptions.AccountExceptions;

/// <summary>
/// Если у пользователя почта неподтверждена
/// </summary>
public class UnconfirmedEmailException : ApplicationBaseException
{
    /// <inheritdoc />
    public UnconfirmedEmailException(string message, HttpStatusCode statusCode = HttpStatusCode.UnprocessableEntity)
        : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public UnconfirmedEmailException()
    {
    }
}