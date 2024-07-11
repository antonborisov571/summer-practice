using AutoMapper;
using Kfoodle.Contracts.Requests.Test.GetCatalog;
using Kfoodle.Contracts.Requests.Test.GetMyTests;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Core.Requests.Test.GetMyTests;

/// <summary>
/// Обработчик для <see cref="GetMyTestsQuery"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="dbContext"></param>
public class GetMyTestsQueryHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    IDbContext dbContext
) : IRequestHandler<GetMyTestsQuery, GetMyTestsResponse>
{
    /// <inheritdoc />
    public async Task<GetMyTestsResponse> Handle(GetMyTestsQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var tests = await dbContext.Tests.Where(x => x.AuthorId == user.Id).ToListAsync();
        
        var testItems = new List<TestItem>();
        
        foreach (var test in tests)
        {
            testItems.Add(new TestItem
            {
                Title = test.Title,
                Description = test.Description ?? "",
                Id = test.Id
            });
        }

        return new GetMyTestsResponse
        {
            TestItems = testItems
        };
    }
}