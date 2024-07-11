using AutoMapper;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Core.Requests.Answer.PostUpdateSingleAnswer;

/// <summary>
/// Обработчик для <see cref="PostUpdateSingleAnswerCommand"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="dbContext"></param>
public class PostUpdateSingleAnswerCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    IDbContext dbContext
) : IRequestHandler<PostUpdateSingleAnswerCommand>
{
    /// <inheritdoc />
    public async Task Handle(PostUpdateSingleAnswerCommand request, CancellationToken cancellationToken)
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
                SingleChoice = await dbContext.Choices.FirstOrDefaultAsync(x => x.Id == request.ChoiceId)
            });
            await dbContext.SaveChangesAsync();
            
            return;
        }

        answer.SingleChoice = await dbContext.Choices.FirstOrDefaultAsync(x => x.Id == request.ChoiceId);
        dbContext.Answers.Update(answer);
        await dbContext.SaveChangesAsync();
    }
}