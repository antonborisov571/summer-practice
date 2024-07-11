using Kfoodle.Contracts.Requests.Test.GetTestResult;
using MediatR;

namespace Kfoodle.Core.Requests.Test.GetTestResult;

/// <summary>
/// Запрос на получение результатов
/// </summary>
public class GetTestResultQuery : IRequest<GetTestResultResponse>
{
    /// <summary>
    /// Констуктор
    /// </summary>
    /// <param name="id"></param>
    public GetTestResultQuery(int id)
    {
        TestAttemptId = id;
    }

    /// <summary>
    /// Конструтор
    /// </summary>
    public GetTestResultQuery()
    {
    }
    
    /// <summary>
    /// Id попытки
    /// </summary>
    public int TestAttemptId { get; set; }
}