using Kfoodle.Contracts.Requests.Auth.PostRegister;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Enums;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Kfoodle.Core.Extensions;
using Kfoodle.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Kfoodle.Core.Requests.Auth.PostRegister;

/// <summary>
/// Обработчик для <see cref="PostRegisterCommand"/>
/// </summary>
/// <param name="userManager">UserManager{User} из Identity</param>
/// <param name="emailSender">Email sender <see cref="IEmailSender"/></param>
/// <param name="logger">Логгер</param>
public class PostRegisterCommandHandler(
    UserManager<User> userManager,
    IEmailSender emailSender,
    ILogger<PostRegisterCommandHandler> logger,
    IHttpContextAccessor httpContextAccessor
    ) : IRequestHandler<PostRegisterCommand, PostRegisterResponse>
{

    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
    public async Task<PostRegisterResponse> Handle(PostRegisterCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Обработка запроса {name}", 
            nameof(PostRegisterCommand));
        
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is not null)
            throw new EmailAlreadyRegisteredException(AuthErrorMessages.UserWithSameEmail);

        user = new User
        {
            Email = request.Email, 
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Birthday = request.Birthday,
            DateRegistration = DateTime.Today,
            
            SecurityStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = false
        };
        
        var result = await userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
            throw new RegisterUserException(
                string.Join("\n", result.Errors.Select(error => error.Description)));
        
        var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var routeValues = new RouteValueDictionary
        {
            ["code"] = confirmationToken,
            ["email"] = request.Email
        };

        if (routeValues["code"] == null || routeValues["email"] == null)
            throw new BadRequestException("Запрос без почты!");
        
        var messageTemplate =
            await EmailTemplateHelper.GetEmailTemplateAsync(Templates.SendEmailConfirmationMessage,
                cancellationToken);

        if (httpContextAccessor.HttpContext == null)
            throw new BadRequestException("Не было запроса!");
        
        var httpRequest = httpContextAccessor.HttpContext.Request;
        var refererUrl = httpRequest.Headers["Referer"].ToString();

        var baseConfirmEmailUrl = "";

        if (Uri.TryCreate(refererUrl, UriKind.Absolute, out Uri? refererUri))
        {
            string port = "";
            if (!(refererUri.IsDefaultPort 
                  || (refererUri.Scheme == "http" && refererUri.Port == 80) 
                  || (refererUri.Scheme == "https" && refererUri.Port == 443)))
            {
                port = $":{refererUri.Port}";
            }
            
            baseConfirmEmailUrl = $"{refererUri.Scheme}://{refererUri.Host}{port}/confirmEmail";
        }
        
        var uriBuilder = new UriBuilder(baseConfirmEmailUrl)
        {
            Query = $"code={Uri.EscapeDataString(routeValues["code"]!.ToString()!)}&email={Uri.EscapeDataString(routeValues["email"]!.ToString()!)}"
        };

        var confirmEmailUrl = uriBuilder.ToString();
        
        var placeholders = new Dictionary<string, string> { ["{confirmationToken}"] = confirmEmailUrl };
        
        var message = messageTemplate.ReplacePlaceholders(placeholders);
            
        await emailSender.SendEmailAsync(user.Email,
            message, cancellationToken);
        
        logger.LogInformation("Обработка запроса {name} завершена" +
                              "Время: {dateTime}", 
            nameof(PostRegisterCommand), DateTime.Now);
        
        return new PostRegisterResponse { Email = request.Email };
    }
}