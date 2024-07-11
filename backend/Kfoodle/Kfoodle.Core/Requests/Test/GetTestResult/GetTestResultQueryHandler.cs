using AutoMapper;
using Kfoodle.Contracts.Requests.Test.GetTestResult;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Core.Requests.Test.GetTestResult;

/// <summary>
/// Обработчик для <see cref="GetTestResultQuery"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="dbContext"></param>
public class GetTestResultQueryHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    IDbContext dbContext
) : IRequestHandler<GetTestResultQuery, GetTestResultResponse>
{
    /// <inheritdoc />
    public async Task<GetTestResultResponse> Handle(GetTestResultQuery request, CancellationToken cancellationToken)
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
            .FirstOrDefaultAsync(x => x.Id == request.TestAttemptId);
        
        if (testAttempt is null)
            throw new BadRequestException("Нет такой попытки");

        return new GetTestResultResponse
        {
            Score = testAttempt.Score,
            Title = testAttempt.Test.Title,
            NumQuestions = testAttempt.Test.NumQuestions ?? testAttempt.Test.Questions.Count,
            NumCorrectAnswers = testAttempt.NumCorrectAnswers,
            StartTime = testAttempt.StartTime,
            EndTime = testAttempt.EndTime
        };
    }
}