using System.Net;

namespace Kfoodle.Core.Exceptions.ChatExceptions;

/// <summary>
/// Не найден HttpContext
/// </summary>
public class NotFoundContextException: ApplicationBaseException
{
    /// <inheritdoc />
    public NotFoundContextException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}