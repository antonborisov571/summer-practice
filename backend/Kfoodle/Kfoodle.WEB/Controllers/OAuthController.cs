using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Requests.OAuth.GetExternalLoginCallback;

namespace Kfoodle.WEB.Controllers;

/// <summary>
/// Контроллер отвечающий за авторизацию и регистрацию через сторонние сервисы 
/// </summary>
[ApiController]
[Route("api/[controller]/")]
[AllowAnonymous]
public class OAuthController : ControllerBase
{
    private readonly IMediator _mediator;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator"></param>
    public OAuthController(IMediator mediator) => _mediator = mediator;

    /// <summary>
    /// Редирект пользователя на логин страницу стороннего сервиса
    /// </summary>
    /// <param name="provider">Имя провайдера(Vkontakte, Yandex, Google)</param>
    /// <param name="signInManager">SignInManager для User</param>
    /// <returns>Challenge Result</returns>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="500">Если не удалось залогиниться через стороннего провайдера</response>
    [HttpGet("ExternalLogin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult ExternalLogin([FromQuery] string provider,
        [FromServices] SignInManager<User> signInManager)
    {
        var httpRequest = HttpContext.Request;
        var refererUrl = httpRequest.Headers["Referer"].ToString();
        
        var encodedReferer = Uri.EscapeDataString(refererUrl);
        
        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "OAuth");
        redirectUrl += $"?referer={encodedReferer}";
        
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }
    
    /// <summary>
    /// Логин через сторонний сервис
    /// </summary>
    /// <returns>GetExternalLoginCallbackResponse(JWT, Refresh Token)</returns>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если не удалось аутентифицироваться через сторонний сервис
    /// или пользовательские данные не удовлетворяют требованиям Identity</response>
    [HttpGet("ExternalLoginCallback")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ExternalLoginCallback(string? referer = null)
    {
        var command = new GetExternalLoginCallbackCommand();
        var response = await _mediator.Send(command);

        var baseUrl = "";
        
        if (Uri.TryCreate(referer, UriKind.Absolute, out Uri? refererUri))
        {
            string port = "";
            if (!(refererUri.IsDefaultPort 
                  || (refererUri.Scheme == "http" && refererUri.Port == 80) 
                  || (refererUri.Scheme == "https" && refererUri.Port == 443)))
            {
                port = $":{refererUri.Port}";
            }
            
            baseUrl = $"{refererUri.Scheme}://{refererUri.Host}{port}/oauth-redirect?accessToken={response.AccessToken}&refreshToken={response.RefreshToken}";
        }
        return new RedirectResult(baseUrl);
    }
}