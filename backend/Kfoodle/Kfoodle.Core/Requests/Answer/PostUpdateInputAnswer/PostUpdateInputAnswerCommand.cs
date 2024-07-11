using Kfoodle.Contracts.Requests.Answer.PostUpdateInputAnswer;
using MediatR;

namespace Kfoodle.Core.Requests.Answer.PostUpdateInputAnswer;

/// <summary>
/// Запрос на обновление ответа
/// </summary>
/// <param name="request"></param>
public class PostUpdateInputAnswerCommand(PostUpdateInputAnswerRequest request)
    : PostUpdateInputAnswerRequest(request), IRequest;