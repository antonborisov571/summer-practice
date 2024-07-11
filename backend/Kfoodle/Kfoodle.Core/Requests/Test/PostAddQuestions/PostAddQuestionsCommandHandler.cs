using AutoMapper;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Kfoodle.Core.Exceptions.TestExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Core.Requests.Test.PostAddQuestions;

/// <summary>
/// Обработчик для <see cref="PostAddQuestionsCommand"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="dbContext"></param>
public class PostAddQuestionsCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    IDbContext dbContext
) : IRequestHandler<PostAddQuestionsCommand>
{
    /// <inheritdoc />
    public async Task Handle(PostAddQuestionsCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var test = await dbContext.Tests
            .AsNoTracking()
            .Include(x => x.Questions)
            .FirstOrDefaultAsync(x => x.Id == request.TestId);

        if (test is null)
            throw new NotFoundException("Тест не найден");

        if (test.AuthorId != user.Id)
            throw new NotAuthoredTestException("Вы не являетесь автором теста");
        
        var sourceTest = await dbContext.Tests
            .AsNoTracking()
            .Include(x => x.Questions)
            .FirstOrDefaultAsync(x => x.Id == request.SourceTestId);
        
        if (sourceTest is null)
            throw new NotFoundException("Тест не найден");
        
        sourceTest.Questions.ForEach(x => x.TestId = test.Id);

        sourceTest.Questions.ForEach(x => x.Id = default);
        
        test.Questions.AddRange(sourceTest.Questions);

        dbContext.Tests.Update(test);

        await dbContext.SaveChangesAsync();
    }
}