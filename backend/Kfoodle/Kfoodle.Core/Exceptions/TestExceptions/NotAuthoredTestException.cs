using System.Net;

namespace Kfoodle.Core.Exceptions.TestExceptions;

/// <inheritdoc />
public class NotAuthoredTestException : ApplicationBaseException
{
    /// <inheritdoc />
    public NotAuthoredTestException(string message) : base(message, HttpStatusCode.Forbidden)
    {
    }
}