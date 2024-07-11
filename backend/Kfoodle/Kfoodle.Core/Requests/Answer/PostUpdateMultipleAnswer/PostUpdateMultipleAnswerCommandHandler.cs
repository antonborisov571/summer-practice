using AutoMapper;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Core.Requests.Answer.PostUpdateMultipleAnswer;

/// <summary>
/// Обработчик для <see cref="PostUpdateMultipleAnswerCommand"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="dbContext"></param>
public class PostUpdateMultipleAnswerCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    IDbContext dbContext
) : IRequestHandler<PostUpdateMultipleAnswerCommand>
{
    /// <inheritdoc />
    public async Task Handle(PostUpdateMultipleAnswerCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var testAttempt = await dbContext.TestAttempts
            .Include(x => x.Test)
            .ThenInclude(x => x.Questions)
            .Include(x => x.Answers)
            .FirstOrDefaultAsync(x => x.Id == request.TestAttemptId);

        if (testAttempt == null)
            throw new BadRequestException("Такой попытки не существует");

        var answer = testAttempt.Answers.FirstOrDefault(x => x.QuestionId == request.QuestionId);

        if (answer == null)
        {
            await dbContext.Answers.AddAsync(new Entities.Answer
            {
                TestAttemptId = testAttempt.Id,
                QuestionId = request.QuestionId,
                ChoicesId = await dbContext.Choices.Where(x => request.ChoicesId.Contains(x.Id)).Select(x => x.Id).ToListAsync()
            });
            await dbContext.SaveChangesAsync();
            
            return;
        }

        answer.ChoicesId = await dbContext.Choices.Where(x => request.ChoicesId.Contains(x.Id)).Select(x => x.Id).ToListAsync();
        dbContext.Answers.Update(answer);
        await dbContext.SaveChangesAsync();
    }
}