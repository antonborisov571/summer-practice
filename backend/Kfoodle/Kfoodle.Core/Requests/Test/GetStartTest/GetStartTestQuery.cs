using Kfoodle.Contracts.Requests.Test.GetStartTest;
using MediatR;

namespace Kfoodle.Core.Requests.Test.GetStartTest;

/// <summary>
/// Запрос на начало тестирование
/// </summary>
public class GetStartTestQuery : IRequest<GetStartTestResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id"></param>
    public GetStartTestQuery(Guid id)
    {
        TestId = id;
    }

    /// <summary>
    /// Констуктор
    /// </summary>
    public GetStartTestQuery()
    {
    }
    
    /// <summary>
    /// Id теста
    /// </summary>
    public Guid TestId { get; set; }
}