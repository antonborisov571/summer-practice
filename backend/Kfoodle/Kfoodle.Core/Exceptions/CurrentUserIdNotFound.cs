using System.Net;

namespace Kfoodle.Core.Exceptions;

/// <summary>
/// Если CurrentUserId из IUserContext равно null
/// </summary>
public class CurrentUserIdNotFound : ApplicationBaseException
{
    /// <inheritdoc />
    public CurrentUserIdNotFound(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    /// <inheritdoc />
    public CurrentUserIdNotFound()
    {
    }
}