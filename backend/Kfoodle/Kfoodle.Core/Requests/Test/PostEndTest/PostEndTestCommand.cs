using Kfoodle.Contracts.Requests.Test.PostEndTest;
using MediatR;

namespace Kfoodle.Core.Requests.Test.PostEndTest;

/// <summary>
/// Запрос о завершении теста
/// </summary>
/// <param name="request"></param>
public class PostEndTestCommand(PostEndTestRequest request)
    : PostEndTestRequest(request), IRequest;