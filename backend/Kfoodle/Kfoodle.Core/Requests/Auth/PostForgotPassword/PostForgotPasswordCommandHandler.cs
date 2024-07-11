using Kfoodle.Core.Abstractions;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Enums;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AccountExceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Kfoodle.Core.Extensions;
using Kfoodle.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Kfoodle.Core.Requests.Auth.PostForgotPassword;


/// <summary>
/// Обработчик для <see cref="PostForgotPasswordCommand"/>
/// </summary>
/// <param name="userManager">UserManager{User} из Identity</param>
/// <param name="emailSender">Email sender <see cref="IEmailSender"/></param>
/// <param name="logger">Логгер</param>
public class PostForgotPasswordCommandHandler(
    UserManager<User> userManager, 
    IEmailSender emailSender,
    ILogger<PostForgotPasswordCommandHandler> logger,
    IHttpContextAccessor httpContextAccessor) : 
    IRequestHandler<PostForgotPasswordCommand>
{
    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>>
    public async Task Handle(PostForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostForgotPasswordCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var user = await userManager.FindByEmailAsync(request.Email);
        
        if (user is null)
            throw new NotFoundUserException(AuthErrorMessages.UserNotFound);

        if (!await userManager.IsEmailConfirmedAsync(user))
            throw new UnconfirmedEmailException(AuthErrorMessages.NotConfirmedEmail);
        
        var confirmationToken = await userManager.GeneratePasswordResetTokenAsync(user);

        var routeValues = new RouteValueDictionary
        {
            ["code"] = confirmationToken,
            ["email"] = request.Email
        };
        
        var messageTemplate =
            await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendForgotPasswordMessage,
                cancellationToken);
        
        if (httpContextAccessor.HttpContext == null)
            throw new BadRequestException("Не было запроса!");
        
        var httpRequest = httpContextAccessor.HttpContext.Request;
        var refererUrl = httpRequest.Headers["Referer"].ToString();

        var resetPasswordUrl = "";

        if (Uri.TryCreate(refererUrl, UriKind.Absolute, out Uri? refererUri))
        {
            string port = "";
            if (!(refererUri.IsDefaultPort 
                  || (refererUri.Scheme == "http" && refererUri.Port == 80) 
                  || (refererUri.Scheme == "https" && refererUri.Port == 443)))
            {
                port = $":{refererUri.Port}";
            }
            
            resetPasswordUrl = $"{refererUri.Scheme}://{refererUri.Host}{port}/resetPassword?code={routeValues["code"]}&email={routeValues["email"]}";
        }
        
        
        var placeholders = new Dictionary<string, string> { ["{confirmationToken}"] = resetPasswordUrl };
        
        var message = messageTemplate.ReplacePlaceholders(placeholders);
            
        await emailSender.SendEmailAsync(user.Email!,
            message, cancellationToken);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostForgotPasswordCommand), DateTime.Now);
    }
}