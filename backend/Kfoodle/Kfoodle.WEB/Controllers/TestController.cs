using Kfoodle.Contracts.Requests.Test.GetCatalog;
using Kfoodle.Contracts.Requests.Test.GetEditQuestions;
using Kfoodle.Contracts.Requests.Test.GetEditTest;
using Kfoodle.Contracts.Requests.Test.GetMyTests;
using Kfoodle.Contracts.Requests.Test.GetPassedTests;
using Kfoodle.Contracts.Requests.Test.GetStartTest;
using Kfoodle.Contracts.Requests.Test.GetTest;
using Kfoodle.Contracts.Requests.Test.GetTestResult;
using Kfoodle.Contracts.Requests.Test.GetUserResults;
using Kfoodle.Contracts.Requests.Test.PatchUpdateQuestions;
using Kfoodle.Contracts.Requests.Test.PatchUpdateTest;
using Kfoodle.Contracts.Requests.Test.PostAddQuestions;
using Kfoodle.Contracts.Requests.Test.PostCreateTest;
using Kfoodle.Contracts.Requests.Test.PostEndTest;
using Kfoodle.Core.Requests.Test.GetCatalog;
using Kfoodle.Core.Requests.Test.GetEditQuestions;
using Kfoodle.Core.Requests.Test.GetEditTest;
using Kfoodle.Core.Requests.Test.GetMyTests;
using Kfoodle.Core.Requests.Test.GetPassedTests;
using Kfoodle.Core.Requests.Test.GetStartTest;
using Kfoodle.Core.Requests.Test.GetTest;
using Kfoodle.Core.Requests.Test.GetTestResult;
using Kfoodle.Core.Requests.Test.GetUserResults;
using Kfoodle.Core.Requests.Test.PatchUpdateQuestions;
using Kfoodle.Core.Requests.Test.PatchUpdateTest;
using Kfoodle.Core.Requests.Test.PostAddQuestions;
using Kfoodle.Core.Requests.Test.PostCreateTest;
using Kfoodle.Core.Requests.Test.PostEndTest;
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
public class TestController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Создание теста
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPost("CreateTest")]
    [ProducesResponseType(typeof(PostCreateTestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<PostCreateTestResponse> CreateTest()
    {
        var command = new PostCreateTestCommand();
        return await mediator.Send(command);
    }
    
    /// <summary>
    /// Получить тест на редактирование
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("{id}/GetEditTest")]
    [ProducesResponseType(typeof(GetEditTestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetEditTestResponse> EditTest(Guid id)
    {
        var command = new GetEditTestQuery(id);
        return await mediator.Send(command);
    }
    
    /// <summary>
    /// Сохранить тест 
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPatch("UpdateTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateTest([FromBody] PatchUpdateTestRequest request)
    {
        var command = new PatchUpdateTestCommand(request);
        await mediator.Send(command);
    }
    
    /// <summary>
    /// Получить тест для редактирования вопросов
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("{id}/GetEditQuestions")]
    [ProducesResponseType(typeof(GetEditQuestionsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetEditQuestionsResponse> GetEditQuestions(Guid id)
    {
        var command = new GetEditQuestionsQuery(id);
        return await mediator.Send(command);
    }
    
    /// <summary>
    /// Сохранить вопросы
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPatch("UpdateQuestions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task UpdateQuestions([FromBody] PatchUpdateQuestionsRequest request)
    {
        var command = new PatchUpdateQuestionsCommand(request);
        await mediator.Send(command);
    }
    
    /// <summary>
    /// Получить тест
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetTestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTestResponse> GetTest(Guid id)
    {
        var command = new GetTestQuery(id);
        return await mediator.Send(command);
    }
    
    /// <summary>
    /// Начать тест
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("{id}/StartTest")]
    [ProducesResponseType(typeof(GetStartTestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetStartTestResponse> StartTest(Guid id)
    {
        var command = new GetStartTestQuery(id);
        return await mediator.Send(command);
    }
    
    /// <summary>
    /// Завершить тест
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPost("{id}/EndTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task EndTest(PostEndTestRequest request)
    {
        var command = new PostEndTestCommand(request);
        await mediator.Send(command);
    }
    
    /// <summary>
    /// Получить результаты теста
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("{id}/GetTestResult")]
    [ProducesResponseType(typeof(GetTestResultResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetTestResultResponse> GetTestResult(int id)
    {
        var command = new GetTestResultQuery(id);
        return await mediator.Send(command);
    }
    
    /// <summary>
    /// Получить пройденные тесты
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("GetPassedTests")]
    [ProducesResponseType(typeof(GetPassedTestsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetPassedTestsResponse> GetPassedTests()
    {
        var command = new GetPassedTestsQuery();
        return await mediator.Send(command);
    }
    
    /// <summary>
    /// Получить доступные тесты
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("GetCatalog")]
    [ProducesResponseType(typeof(GetCatalogResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetCatalogResponse> GetCatalog()
    {
        var command = new GetCatalogQuery();
        return await mediator.Send(command);
    }
    
    /// <summary>
    /// Получить мои тесты
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("GetMyTests")]
    [ProducesResponseType(typeof(GetMyTestsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetMyTestsResponse> GetMyTests()
    {
        var command = new GetMyTestsQuery();
        return await mediator.Send(command);
    }
    
    /// <summary>
    /// Получить результаты тестов
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpGet("{id}/GetUserResults")]
    [ProducesResponseType(typeof(GetUserResultsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUserResultsResponse> GetUserResults(Guid id)
    {
        var command = new GetUserResultsQuery(id);
        return await mediator.Send(command);
    }
    
    /// <summary>
    /// Добавить вопросы из другого теста
    /// </summary>
    /// <response code="200">Если всё хорошо</response>
    /// <response code="400">Если пользователь с CurrentUserId из JWT Claims не найден</response>
    /// <response code="401">Если пользователь не авторизован</response>
    [HttpPost("{id}/AddQuestionsFromTest")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task AddQuestionsFromTest(Guid id, [FromBody] PostAddQuestionsRequest request)
    {
        var command = new PostAddQuestionsCommand(id, request);
        await mediator.Send(command);
    }
}