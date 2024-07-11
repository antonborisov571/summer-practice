using System.Net;

namespace Kfoodle.Core.Exceptions;

/// <summary>
/// Не найден
/// </summary>
public class NotFoundException : ApplicationBaseException
{
    /// <inheritdoc />
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}