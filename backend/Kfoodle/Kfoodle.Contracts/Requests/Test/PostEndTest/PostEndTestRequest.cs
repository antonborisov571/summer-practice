namespace Kfoodle.Contracts.Requests.Test.PostEndTest;

/// <summary>
/// Запрос на завершении теста
/// </summary>
public class PostEndTestRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"></param>
    public PostEndTestRequest(PostEndTestRequest request)
    {
        TestId = request.TestId;
        TestAttemptId = request.TestAttemptId;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostEndTestRequest()
    {
    }
    
    /// <summary>
    /// Id теста
    /// </summary>
    public Guid TestId { get; set; }
    
    /// <summary>
    /// Id попытки
    /// </summary>
    public int TestAttemptId { get; set; }
}