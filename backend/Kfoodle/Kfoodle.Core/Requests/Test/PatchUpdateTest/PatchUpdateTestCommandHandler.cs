using AutoMapper;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Kfoodle.Core.Exceptions;
using Kfoodle.Core.Exceptions.AuthExceptions;
using Kfoodle.Core.Exceptions.TestExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Kfoodle.Core.Requests.Test.PatchUpdateTest;

/// <summary>
/// Обработчик для <see cref="PatchUpdateTestCommand"/>
/// </summary>
/// <param name="userManager"></param>
/// <param name="userContext"></param>
/// <param name="testRepository"></param>
public class PatchUpdateTestCommandHandler(
    UserManager<User> userManager, 
    IUserContext userContext,
    AbstractTestRepository testRepository
) : IRequestHandler<PatchUpdateTestCommand>
{
    /// <inheritdoc />
    public async Task Handle(PatchUpdateTestCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("Нет пользователя");

        var user = await userManager.FindByIdAsync(userId.ToString()!);

        if (user is null)
            throw new NotFoundUserException($"User with id: {userId}");

        var test = await testRepository.GetTest(request.Id);

        if (test is null)
            throw new NotFoundException("Тест не найден");

        if (test.AuthorId != user.Id)
            throw new NotAuthoredTestException("Вы не являетесь автором теста");

        test.Title = request.Title;
        test.Description = request.Description;
        test.EndDate = request.EndDate;
        test.Duration = request.Duration;
        test.NumQuestions = request.NumQuestions;
        test.MaxAttempts = request.MaxAttempts;

        await testRepository.Update(test);
    }
}