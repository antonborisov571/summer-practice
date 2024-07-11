using System.Net;

namespace Kfoodle.Core.Exceptions.AuthExceptions;

/// <summary>
/// Если новый пароль и старый совпадают
/// </summary>
public class EqualsOldAndNewPasswordsException : ApplicationBaseException
{
    /// <inheritdoc />
    public EqualsOldAndNewPasswordsException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public EqualsOldAndNewPasswordsException()
    {
    }
}