using AutoMapper;
using Kfoodle.Contracts.Requests.Test.GetPassedTests;
using Kfoodle.Contracts.Requests.Test.GetTestResult;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Core.Requests.Test.GetPassedTests;

/// <summary>
/// Обработчик для <see cref="GetPassedTestsQuery"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="dbContext"></param>
public class GetPassedTestsQueryHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    IDbContext dbContext
) : IRequestHandler<GetPassedTestsQuery, GetPassedTestsResponse>
{
    /// <inheritdoc />
    public async Task<GetPassedTestsResponse> Handle(GetPassedTestsQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var testAttempts = await dbContext.TestAttempts
            .Include(x => x.Test)
            .ThenInclude(x => x.Questions)
            .Where(x => x.UserId == user.Id)
            .ToListAsync();

        var testResults = new List<TestResult>();

        foreach (var testAttempt in testAttempts)
        {
            testResults.Add( new TestResult
            {
                Score = testAttempt.Score,
                Title = testAttempt.Test.Title,
                NumQuestions = testAttempt.Test.NumQuestions ?? testAttempt.Test.Questions.Count,
                NumCorrectAnswers = testAttempt.NumCorrectAnswers,
                StartTime = testAttempt.StartTime,
                EndTime = testAttempt.EndTime
            });
        }

        return new GetPassedTestsResponse { TestResults = testResults };
    }
}