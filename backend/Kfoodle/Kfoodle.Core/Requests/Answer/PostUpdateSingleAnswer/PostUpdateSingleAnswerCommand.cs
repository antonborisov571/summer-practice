using Kfoodle.Contracts.Requests.Answer.PostUpdateSingleAnswer;
using MediatR;

namespace Kfoodle.Core.Requests.Answer.PostUpdateSingleAnswer;

/// <summary>
/// Запрос на обновления ответа в задание с ед. ответом
/// </summary>
/// <param name="request"></param>
public class PostUpdateSingleAnswerCommand(PostUpdateSingleAnswerRequest request)
    : PostUpdateSingleAnswerRequest(request), IRequest;