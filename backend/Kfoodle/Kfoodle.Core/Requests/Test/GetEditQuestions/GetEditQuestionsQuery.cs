using Kfoodle.Contracts.Requests.Test.GetEditQuestions;
using MediatR;

namespace Kfoodle.Core.Requests.Test.GetEditQuestions;

/// <summary>
/// Запрос на изменение вопросов
/// </summary>
public class GetEditQuestionsQuery : IRequest<GetEditQuestionsResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id"></param>
    public GetEditQuestionsQuery(Guid id)
    {
        TestId = id;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetEditQuestionsQuery()
    {
    }
    
    /// <summary>
    /// Id теста
    /// </summary>
    public Guid TestId { get; set; }
}