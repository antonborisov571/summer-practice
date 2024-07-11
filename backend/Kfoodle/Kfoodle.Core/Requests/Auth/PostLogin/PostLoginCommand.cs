using Kfoodle.Contracts.Requests.Auth.PostLogin;
using MediatR;

namespace Kfoodle.Core.Requests.Auth.PostLogin;

/// <summary>
/// Команда для авторизации пользователя
/// </summary>
public class PostLoginCommand:  PostLoginRequest, IRequest<PostLoginResponse>
{
    /// <inheritdoc />
    public PostLoginCommand(PostLoginRequest request) : base(request)
    {
    }

    /// <inheritdoc />
    public PostLoginCommand()
    {
    }
}