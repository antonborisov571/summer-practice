using System.Net;

namespace Kfoodle.Core.Exceptions.AuthExceptions;

/// <summary>
/// Если токен, который был отправлен на почту и токен, отправленный пользователем не совпадают
/// </summary>
public class WrongConfirmationTokenException : ApplicationBaseException
{
    /// <inheritdoc />
    public WrongConfirmationTokenException(string message,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public WrongConfirmationTokenException()
    {
    }
}