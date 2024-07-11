using Kfoodle.Contracts.Requests.Account.PostRefreshToken;
using Kfoodle.Contracts.Requests.Auth.PostRefreshToken;
using MediatR;

namespace Kfoodle.Core.Requests.Auth.PostRefreshToken;

/// <summary>
/// Команда для обновления JWT токена
/// </summary>
public class PostRefreshTokenCommand : PostRefreshTokenRequest, IRequest<PostRefreshTokenResponse>
{
    /// <inheritdoc />
    public PostRefreshTokenCommand(PostRefreshTokenRequest request) : base(request)
    {
    }

    /// <inheritdoc />
    public PostRefreshTokenCommand()
    {
    }
}