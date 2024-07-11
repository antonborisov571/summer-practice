using Kfoodle.Contracts.Requests.OAuth.GetExternalLoginCallback;
using MediatR;

namespace Kfoodle.Core.Requests.OAuth.GetExternalLoginCallback;

/// <summary>
/// Команда для авторизации через сторонние сервисы
/// </summary>
public class GetExternalLoginCallbackCommand : IRequest<GetExternalLoginCallbackResponse>;