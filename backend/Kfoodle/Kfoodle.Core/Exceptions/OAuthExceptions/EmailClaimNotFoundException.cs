using System.Net;

namespace Kfoodle.Core.Exceptions.OAuthAccountExceptions;

/// <summary>
/// Если не найден Claim с типом Email
/// </summary>
public class EmailClaimNotFoundException : ApplicationBaseException
{
    /// <inheritdoc />
    public EmailClaimNotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }
}