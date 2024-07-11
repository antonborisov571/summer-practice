using MediatR;
using Kfoodle.Contracts.Requests.Account.PatchUpdateUserInfo;

namespace Kfoodle.Core.Requests.Account.PatchUpdateUserInfo;

/// <summary>
/// Запрос на обновление данных о пользователе
/// </summary>
public class PatchUpdateUserInfoCommand : PatchUpdateUserInfoRequest, IRequest<PatchUpdateUserInfoResponse> 
{
    /// <inheritdoc />
    public PatchUpdateUserInfoCommand(PatchUpdateUserInfoRequest request) : base(request)
    {
    }
}