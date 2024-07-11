using System.Net;

namespace Kfoodle.Core.Exceptions;

/// <summary>
/// Плохой запрос
/// </summary>
public class BadRequestException : ApplicationBaseException
{
    /// <inheritdoc />
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}