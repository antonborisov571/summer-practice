﻿using Kfoodle.Core.Abstractions;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AccountExceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Kfoodle.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using File = Kfoodle.Core.Entities.File;

namespace Kfoodle.Core.Requests.Account.PatchUpdateAvatar;

/// <summary>
/// Обработчик для <see cref="PatchUpdateAvatarCommand"/>
/// </summary>
/// <param name="userManager">UserManager{User} из Identity</param>
/// <param name="userContext">UserContext <see cref="IUserContext"/></param>
/// <param name="sftpService">Хранилище данных</param>
/// <param name="filesRepository">Репозиторий для файлов</param>
/// <param name="logger">Логгер</param>
public class PatchUpdateAvatarCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    ISftpService sftpService,
    AbstractFilesRepository filesRepository,
    ILogger<PatchUpdateAvatarCommandHandler> logger
    ) : IRequestHandler<PatchUpdateAvatarCommand>
{
    /// <inheritdoc />
    public async Task Handle(PatchUpdateAvatarCommand request, CancellationToken cancellationToken )
    {
        logger.LogInformation("Обработка запроса {name} для пользователя: {UserId}", 
            nameof(PatchUpdateAvatarCommand), userContext.CurrentUserId);
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("User Id не был найден");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"Пользователь с id: {userId}");

        await using var memoryStream = request.FormFile.OpenReadStream();

        var address = await sftpService.UploadAsync(
            new FileContent(
                memoryStream,
                request.FormFile.FileName,
                request.FormFile.ContentType),
            cancellationToken: cancellationToken);
        

        await filesRepository.AddAsync(new File(
            request.FormFile.FileName, 
            request.FormFile.ContentType, 
            address, 
            request.FormFile.Length));
        
        user.Avatar = address;
        await userManager.UpdateAsync(user);
        
        logger.LogInformation("Обработка запроса {name} " +
                              "завершена успешно для пользователя: {UserId}." +
                              "Время: {dateTime}", 
            nameof(PatchUpdateAvatarCommand), userContext.CurrentUserId, DateTime.Now);
    }
}