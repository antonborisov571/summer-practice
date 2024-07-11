using MediatR;
using Kfoodle.Contracts.Requests.Auth.PostRegister;

namespace Kfoodle.Core.Requests.Auth.PostRegister;

/// <summary>
/// Команда для регистрации пользователя
/// </summary>
public class PostRegisterCommand: PostRegisterRequest, IRequest<PostRegisterResponse>
{
    /// <inheritdoc />
    public PostRegisterCommand(PostRegisterRequest request) : base(request)
    {
    }

    /// <inheritdoc />
    public PostRegisterCommand()
    {
    }
}