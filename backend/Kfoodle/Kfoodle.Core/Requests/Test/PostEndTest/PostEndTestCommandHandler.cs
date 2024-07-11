using AutoMapper;
using Kfoodle.Contracts.Enums;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Enums;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Core.Requests.Test.PostEndTest;

/// <summary>
/// Обработчик для <see cref="PostEndTestCommand"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="testRepository"></param>
/// <param name="dbContext"></param>
public class PostEndTestCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    AbstractTestRepository testRepository,
    IDbContext dbContext
) : IRequestHandler<PostEndTestCommand>
{
    /// <inheritdoc />
    public async Task Handle(PostEndTestCommand request, CancellationToken cancellationToken)
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

        var testAttempt = await dbContext.TestAttempts
            .Include(x => x.User)
            .Include(x => x.Test)
            .ThenInclude(x => x.Questions)
            .Include(x => x.Answers)
            .ThenInclude(x => x.Question)
            .ThenInclude(x => x.Choices)
            .FirstOrDefaultAsync(x => x.Id == request.TestAttemptId);

        if (testAttempt is null)
            throw new BadRequestException("Нет такой попытки");
        
        testAttempt.EndTime = DateTime.Now;

        testAttempt.Score = testAttempt.Answers
            .Count(x => (x.InputAnswer == x.Question.RightAnswer && x.Question.QuestionType == QuestionType.InputAnswer) ||
                        (x.Question.Choices != null && x.SingleChoice != null && x.Question.QuestionType == QuestionType.SingleAnswer &&
                         (bool)x.Question.Choices.FirstOrDefault(y => y.Id == x.SingleChoice.Id)?.IsCorrect) ||
                         (x.Question.Choices != null && x.Question.QuestionType == QuestionType.MultipleAnswers &&
                          x.Question.Choices.Where(y => y.IsCorrect).Count() == x.ChoicesId.Count &&
                          x.Question.Choices.Where(y => x.ChoicesId.Contains(y.Id)).All(y => y.IsCorrect)));
        
        testAttempt.NumCorrectAnswers = testAttempt.Score;

        await dbContext.SaveChangesAsync();


    }
}