using Kfoodle.Contracts.Requests.Answer.PostUpdateMultipleAnswer;
using Kfoodle.Contracts.Requests.Answer.PostUpdateSingleAnswer;
using MediatR;

namespace Kfoodle.Core.Requests.Answer.PostUpdateMultipleAnswer;

/// <summary>
/// Запрос на обновление ответа в задание с мн. выбором
/// </summary>
/// <param name="request"></param>
public class PostUpdateMultipleAnswerCommand(PostUpdateMultipleAnswerRequest request)
    : PostUpdateMultipleAnswerRequest(request), IRequest;