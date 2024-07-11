using Kfoodle.Contracts.Requests.Test.GetTest;
using MediatR;

namespace Kfoodle.Core.Requests.Test.GetTest;

/// <summary>
/// Запрос на получение теста
/// </summary>
public class GetTestQuery : IRequest<GetTestResponse>
{
    /// <summary>
    /// Констуктор
    /// </summary>
    /// <param name="id"></param>
    public GetTestQuery(Guid id)
    {
        TestId = id;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetTestQuery()
    {
    }
    
    /// <summary>
    /// Id теста
    /// </summary>
    public Guid TestId { get; set; }
}