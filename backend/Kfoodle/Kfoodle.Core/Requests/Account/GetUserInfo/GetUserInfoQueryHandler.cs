using MediatR;
using Microsoft.AspNetCore.Identity;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Kfoodle.Core.Models;
using Kfoodle.Contracts.Requests.Account.GetUserInfo;
using Microsoft.Extensions.Logging;

namespace Kfoodle.Core.Requests.Account.GetUserInfo;


/// <summary>
/// Обработчик для <see cref="GetUserInfoQuery"/>
/// </summary>
/// <param name="userManager">UserManager</param>
/// <param name="userContext">UserContext</param>
/// <param name="sftpService">Sftp service</param>
/// <param name="logger">Логгер</param>
public class GetUserInfoQueryHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    ISftpService sftpService,
    ILogger<GetUserInfoQueryHandler> logger
    ) : IRequestHandler<GetUserInfoQuery, GetUserInfoResponse>
{
    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
    public async Task<GetUserInfoResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name} для пользователя: {UserId}", 
            nameof(GetUserInfoQuery), userContext.CurrentUserId);
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        using var memoryStream = new MemoryStream();
        if (user.Avatar != null)
        {
            FileContent? avatar;
            avatar = await sftpService.DownloadFileAsync(user.Avatar, cancellationToken);

            if (avatar != null)
                await avatar.Content.CopyToAsync(memoryStream, cancellationToken);
        }
        
        logger.LogInformation("Обработка запроса {name} " +
                              "завершена успешно для пользователя: {UserId}." +
                              "Время: {dateTime}", 
            nameof(GetUserInfoQuery), userContext.CurrentUserId, DateTime.Now);
        
        return new GetUserInfoResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Birthday = user.Birthday,
            Avatar = Convert.ToBase64String(memoryStream.ToArray()),
            DateRegistration = user.DateRegistration,
            Email = user.Email!,
            TwoFactorEnabled = user.TwoFactorEnabled
        };
    }
}