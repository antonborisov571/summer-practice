using AutoMapper;
using Kfoodle.Contracts.Requests.Test.GetTest;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Core.Requests.Test.GetTest;

/// <summary>
/// Обработчик для <see cref="GetTestQuery"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="testRepository"></param>
/// <param name="mapper"></param>
/// <param name="dbContext"></param>
public class GetTestQueryHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    AbstractTestRepository testRepository,
    IMapper mapper,
    IDbContext dbContext
) : IRequestHandler<GetTestQuery, GetTestResponse>
{
    /// <inheritdoc />
    public async Task<GetTestResponse> Handle(GetTestQuery request, CancellationToken cancellationToken)
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

        var testAttempts = await dbContext.TestAttempts
            .Include(x => x.Answers)
            .Where(x => x.TestId == test.Id && x.UserId == user.Id)
            .ToListAsync();

        if (testAttempts.Count > test.MaxAttempts && test.MaxAttempts != 0)
            throw new BadRequestException("Превышено число попыток");

        var testAttempt = testAttempts.FirstOrDefault(x => x.EndTime > DateTime.Now);
        
        if (testAttempt == null)
            return new GetTestResponse
            {
                Id = test.Id,
                IsAccess = false,
                Title = test.Title,
                Description = test.Description,
                EndDate = test.EndDate,
                Duration = test.Duration,
                NumQuestions = test.NumQuestions,
                MaxAttempts = test.MaxAttempts
            };

        var response = mapper.Map<GetTestResponse>(test);
        response.IsAccess = true;
        response.TestAttemptId = testAttempt.Id;
        response.StartTime = testAttempt.StartTime;
        
        var numQuestions = test.NumQuestions > 0 ? (int)test.NumQuestions : test.Questions.Count;

        var allQuestionIndexes = Enumerable.Range(0, test.Questions.Count).ToList();
        
        var random = new Random();

        var randomIndexes = allQuestionIndexes.OrderBy(x => random.Next()).Take(numQuestions).ToList();

        var selectedQuestions = new List<QuestionItem>();
        
        foreach (int index in randomIndexes)
        {
            var question = response.Questions[index];
            selectedQuestions.Add(question);
        }

        foreach (var question in selectedQuestions)
        {
            var answer = testAttempt.Answers.FirstOrDefault(x => x.QuestionId == question.Id);

            if (answer != null)
            {
                question.Answer = answer.InputAnswer;

                if (question.Choices != null && question.Choices.Any())
                {
                    foreach (var choice in question.Choices)
                    {
                        choice.IsSelected = 
                            choice.Id == answer.SingleChoice?.Id ||
                            (bool)answer.ChoicesId?.Any(x => x == choice.Id);
                    }
                }
            }
        }

        response.Questions = selectedQuestions;

        return response;
    }
}