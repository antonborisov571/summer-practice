using Kfoodle.Contracts.Requests.Test.GetEditTest;
using MediatR;

namespace Kfoodle.Core.Requests.Test.GetEditTest;

/// <summary>
/// Запрос на изменение теста
/// </summary>
public class GetEditTestQuery : IRequest<GetEditTestResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id"></param>
    public GetEditTestQuery(Guid id)
    {
        TestId = id;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetEditTestQuery()
    {
    }
    
    /// <summary>
    /// Id теста
    /// </summary>
    public Guid TestId { get; set; }
}