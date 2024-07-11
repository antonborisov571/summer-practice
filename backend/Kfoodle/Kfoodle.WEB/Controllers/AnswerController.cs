using Kfoodle.Contracts.Requests.Answer.PostUpdateInputAnswer;
using Kfoodle.Contracts.Requests.Answer.PostUpdateMultipleAnswer;
using Kfoodle.Contracts.Requests.Answer.PostUpdateSingleAnswer;
using Kfoodle.Core.Requests.Answer.PostUpdateInputAnswer;
using Kfoodle.Core.Requests.Answer.PostUpdateMultipleAnswer;
using Kfoodle.Core.Requests.Answer.PostUpdateSingleAnswer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kfoodle.WEB.Controllers;

/// <summary>
/// Контроллер отвечающий за действия с тестами
/// </summary>
/// <param name="mediator">Медиатор из библиотеки MediatR</param>
[ApiController]
[Authorize]
[Route("api/[controller]/")]
public class AnswerController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Выбор ответа в задание с единственным ответом
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPost("SingleAnswer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateSingleAnswer([FromBody] PostUpdateSingleAnswerRequest request)
    {
        var command = new PostUpdateSingleAnswerCommand(request);
        await mediator.Send(command);
    }
    
    /// <summary>
    /// Выбор ответа в задание с множественным выбором
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPost("MultipleAnswer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateMultipleAnswer([FromBody] PostUpdateMultipleAnswerRequest request)
    {
        var command = new PostUpdateMultipleAnswerCommand(request);
        await mediator.Send(command);
    }
    
    /// <summary>
    /// Отправка введенного ответа
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPost("InputAnswer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateInputAnswer([FromBody] PostUpdateInputAnswerRequest request)
    {
        var command = new PostUpdateInputAnswerCommand(request);
        await mediator.Send(command);
    }
}