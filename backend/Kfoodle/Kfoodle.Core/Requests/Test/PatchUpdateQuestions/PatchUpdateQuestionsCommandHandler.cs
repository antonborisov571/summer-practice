using AutoMapper;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Enums;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Kfoodle.Core.Exceptions.TestExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kfoodle.Core.Requests.Test.PatchUpdateQuestions;

/// <summary>
/// Обработчик для <see cref="PatchUpdateQuestionsCommand"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="testRepository"></param>
/// <param name="dbContext"></param>
public class PatchUpdateQuestionsCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    AbstractTestRepository testRepository,
    IDbContext dbContext
) : IRequestHandler<PatchUpdateQuestionsCommand>
{
    /// <inheritdoc />
    public async Task Handle(PatchUpdateQuestionsCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var test = await testRepository.GetTestWithQuestions(request.Id);

        if (test is null)
            throw new NotFoundException("Тест не найден");

        if (test.AuthorId != user.Id)
            throw new NotAuthoredTestException("Вы не являетесь автором теста");
        
        dbContext.Questions.RemoveRange(dbContext.Questions.Where(x => x.TestId == test.Id));
        await dbContext.SaveChangesAsync(cancellationToken);
        foreach (var updatedQuestion in request.Questions)
        {
            var question = new Question
            {
                TestId = test.Id,
                QuestionType = updatedQuestion.QuestionType,
                QuestionText = updatedQuestion.QuestionText,
                RightAnswer = updatedQuestion.RightAnswer
            };
            await dbContext.Questions.AddAsync(question, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            if (updatedQuestion.Choices != null)
                foreach (var addedChoice in updatedQuestion.Choices)
                {
                    await dbContext.Choices.AddAsync(new Choice
                    {
                        QuestionId = question.Id,
                        ChoiceText = addedChoice.ChoiceText,
                        IsCorrect = addedChoice.IsCorrect
                    }, cancellationToken);
                }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}