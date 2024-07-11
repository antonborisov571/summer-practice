using Kfoodle.Contracts.Requests.Auth.PostRevokeToken;
using MediatR;

namespace Kfoodle.Core.Requests.Auth.PostRevokeToken;

/// <summary>
/// Команда для обнуления Refresh токена
/// </summary>
public class PostRevokeTokenCommand : PostRevokeTokenRequest, IRequest
{
    /// <inheritdoc />
    public PostRevokeTokenCommand(PostRevokeTokenRequest request) : base(request)
    {
    }

    /// <inheritdoc />
    public PostRevokeTokenCommand()
    {
    }
}