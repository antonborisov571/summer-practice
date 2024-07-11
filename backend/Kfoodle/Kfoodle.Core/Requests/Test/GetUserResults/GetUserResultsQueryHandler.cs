using AutoMapper;
using Kfoodle.Contracts.Requests.Test.GetUserResults;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Kfoodle.Core.Exceptions.TestExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Core.Requests.Test.GetUserResults;

/// <summary>
/// Обработчик для <see cref="GetUserResultsQueryHandler"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="dbContext"></param>
public class GetUserResultsQueryHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    IDbContext dbContext
) : IRequestHandler<GetUserResultsQuery, GetUserResultsResponse>
{
    /// <inheritdoc />
    public async Task<GetUserResultsResponse> Handle(GetUserResultsQuery request, CancellationToken cancellationToken)
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
            .Include(x => x.TestAttempts)
            .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == request.TestId);

        if (test is null)
            throw new NotFoundException("Тест не найден");

        if (test.AuthorId != user.Id)
            throw new NotAuthoredTestException("Вы не являетесь автором теста");

        var userResults = new List<UserResult>();

        foreach (var testAttempt in test.TestAttempts)
        {
            userResults.Add(new UserResult
            {
                FirstName = testAttempt.User.FirstName,
                LastName = testAttempt.User.LastName,
                Score = testAttempt.Score,
                StartTime = testAttempt.StartTime,
                EndTime = testAttempt.EndTime
            });
        }

        return new GetUserResultsResponse
        {
            Title = test.Title,
            UserResults = userResults
        };
    }
}