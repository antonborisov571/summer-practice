using Kfoodle.Contracts.Requests.Test.GetUserResults;
using MediatR;

namespace Kfoodle.Core.Requests.Test.GetUserResults;

/// <summary>
/// Запрос на получение результатов пользователей
/// </summary>
public class GetUserResultsQuery : IRequest<GetUserResultsResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id"></param>
    public GetUserResultsQuery(Guid id)
    {
        TestId = id;
    }
    
    /// <summary>
    /// Id теста
    /// </summary>
    public Guid TestId { get; set; }
}