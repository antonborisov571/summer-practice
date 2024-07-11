using Kfoodle.Contracts.Requests.Test.PostCreateTest;
using MediatR;

namespace Kfoodle.Core.Requests.Test.PostCreateTest;

/// <summary>
/// Запрос на создание теста
/// </summary>
public class PostCreateTestCommand : IRequest<PostCreateTestResponse>;