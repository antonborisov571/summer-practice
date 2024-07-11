using AutoMapper;
using Kfoodle.Contracts.Requests.Test.GetEditTest;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Kfoodle.Core.Exceptions.TestExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kfoodle.Core.Requests.Test.GetEditTest;

/// <summary>
/// Обработчик для <see cref="GetEditTestQuery"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="testRepository"></param>
/// <param name="mapper"></param>
public class GetEditTestQueryHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    AbstractTestRepository testRepository,
    IMapper mapper
) : IRequestHandler<GetEditTestQuery, GetEditTestResponse>
{
    /// <inheritdoc />
    public async Task<GetEditTestResponse> Handle(GetEditTestQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var test = await testRepository.GetTest(request.TestId);

        if (test is null)
            throw new NotFoundException("Тест не найден");

        if (test.AuthorId != user.Id)
            throw new NotAuthoredTestException("Вы не являетесь автором теста");

        return mapper.Map<GetEditTestResponse>(test);
    }
}