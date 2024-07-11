using MediatR;
using Kfoodle.Contracts.Requests.Auth.PostResetPassword;

namespace Kfoodle.Core.Requests.Auth.PostResetPassword;

/// <summary>
/// Команда для сброса пароля
/// </summary>
public class PostResetPasswordCommand : PostResetPasswordRequest, IRequest<PostResetPasswordResponse>
{
    /// <inheritdoc />
    public PostResetPasswordCommand(PostResetPasswordRequest request) : base(request)
    {
    }
}