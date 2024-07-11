using Kfoodle.Contracts.Requests.Auth.PostConfirmPasswordReset;
using MediatR;

namespace Kfoodle.Core.Requests.Auth.PostConfirmPasswordReset;

/// <summary>
/// Команда на подтверждение сброса пароля
/// </summary>
public class PostConfirmPasswordResetCommand : PostConfirmPasswordResetRequest, IRequest
{
    /// <inheritdoc />
    public PostConfirmPasswordResetCommand(PostConfirmPasswordResetRequest request) : base(request)
    {
    }

    /// <inheritdoc />
    public PostConfirmPasswordResetCommand()
    {
    }
}