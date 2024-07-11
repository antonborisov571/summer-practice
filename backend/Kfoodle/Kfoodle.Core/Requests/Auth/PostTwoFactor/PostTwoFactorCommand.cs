using Kfoodle.Contracts.Requests.Auth.PostTwoFactor;
using MediatR;

namespace Kfoodle.Core.Requests.Auth.PostTwoFactor;

/// <summary>
/// Команда для двухфакторки
/// </summary>
/// <param name="request"></param>
public class PostTwoFactorCommand(PostTwoFactorRequest request)
    : PostTwoFactorRequest(request), IRequest<PostTwoFactorResponse>;