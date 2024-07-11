using Kfoodle.Contracts.Requests.Test.PatchUpdateQuestions;
using MediatR;

namespace Kfoodle.Core.Requests.Test.PatchUpdateQuestions;

/// <summary>
/// Запрос на обновление вопросов
/// </summary>
/// <param name="request"></param>
public class PatchUpdateQuestionsCommand(PatchUpdateQuestionsRequest request)
    : PatchUpdateQuestionsRequest(request), IRequest;