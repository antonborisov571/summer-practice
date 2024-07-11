using AutoMapper;
using Kfoodle.Contracts.Requests.Test.GetEditQuestions;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Kfoodle.Core.Exceptions.TestExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kfoodle.Core.Requests.Test.GetEditQuestions;

/// <summary>
/// Обработчик для <see cref="GetEditQuestionsQuery"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="testRepository"></param>
/// <param name="mapper"></param>
public class GetEditQuestionsQueryHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    AbstractTestRepository testRepository,
    IMapper mapper
) : IRequestHandler<GetEditQuestionsQuery, GetEditQuestionsResponse>
{
    /// <inheritdoc />
    public async Task<GetEditQuestionsResponse> Handle(GetEditQuestionsQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var test = await testRepository.GetTestWithQuestions(request.TestId);

        if (test is null)
            throw new NotFoundException("Тест не найден");

        if (test.AuthorId != user.Id)
            throw new NotAuthoredTestException("Вы не являетесь автором теста");

        return mapper.Map<GetEditQuestionsResponse>(test);
    }
}