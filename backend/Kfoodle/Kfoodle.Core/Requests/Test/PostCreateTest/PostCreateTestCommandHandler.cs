using Kfoodle.Contracts.Requests.Test.PostCreateTest;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kfoodle.Core.Requests.Test.PostCreateTest;

/// <summary>
/// Обработчик для <see cref="PostCreateTestCommand"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="testRepository"></param>
public class PostCreateTestCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    AbstractTestRepository testRepository
) : IRequestHandler<PostCreateTestCommand, PostCreateTestResponse>
{
    /// <inheritdoc />
    public async Task<PostCreateTestResponse> Handle(PostCreateTestCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var test = new Entities.Test
        {
            Title = "Без названия",
            Description = "Описание",
            EndDate = null,
            Duration = 60,
            NumQuestions = 0,
            AuthorId = user.Id,
            MaxAttempts = 0
        };

        await testRepository.AddAsync(test);
        
        return new PostCreateTestResponse
        {
            TestId = test.Id
        };
    }
}