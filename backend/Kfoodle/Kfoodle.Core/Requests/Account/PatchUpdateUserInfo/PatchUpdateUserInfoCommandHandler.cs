using MediatR;
using Microsoft.AspNetCore.Identity;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Enums;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Models;
using Kfoodle.Contracts.Requests.Account.PatchUpdateUserInfo;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Microsoft.Extensions.Logging;

namespace Kfoodle.Core.Requests.Account.PatchUpdateUserInfo;

/// <summary>
/// Обработчик для <see cref="PatchUpdateUserInfoCommand"/>
/// </summary>
/// <param name="userManager">UserManager{User} из Identity</param>
/// <param name="userContext">UserContext <see cref="IUserContext"/></param>
/// <param name="claimsManager">Claims Manager <see cref="IUserClaimsManager"/>/></param>
/// <param name="jwtGenerator">Генератор JWT токенов</param>
/// <param name="emailSender">Email sender <see cref="IEmailSender"/></param>
/// <param name="logger">Логгер</param>
public class PatchUpdateUserInfoCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    IUserClaimsManager claimsManager, 
    IJwtGenerator jwtGenerator, 
    IEmailSender emailSender,
    ILogger<PatchUpdateUserInfoCommandHandler> logger) 
    : IRequestHandler<PatchUpdateUserInfoCommand, PatchUpdateUserInfoResponse>
{
    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
    public async Task<PatchUpdateUserInfoResponse> Handle(PatchUpdateUserInfoCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name} для пользователя: {UserId}", 
            nameof(PatchUpdateUserInfoCommand), userContext.CurrentUserId);
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("User Id не был найден");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"Пользователь с id: {userId}");

        user.FirstName = request.FirstName ?? user.FirstName;

        user.LastName = request.LastName ?? user.LastName;
        
        var claims = await claimsManager.GetUserClaimsAsync(user, cancellationToken);

        user.AccessToken = jwtGenerator.GenerateToken(claims);
        user.RefreshToken = jwtGenerator.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(TokenConfiguration.RefreshTokenExpiryDays);

        var message =
            await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendUserInfoUpdatedNotification,
                cancellationToken);
        
        await userManager.UpdateAsync(user);
        await emailSender.SendEmailAsync(user.Email!, message, cancellationToken);
        
        logger.LogInformation("Обработка запроса {name} " +
                              "завершена успешно для пользователя: {UserId}." +
                              "Время: {dateTime}", 
            nameof(PatchUpdateUserInfoCommand), userContext.CurrentUserId, DateTime.Now);

        return new PatchUpdateUserInfoResponse { AccessToken = user.AccessToken, RefreshToken = user.RefreshToken };
    }
}