namespace Kfoodle.Contracts.Requests.Answer.PostUpdateMultipleAnswer;

/// <summary>
/// Обновить ответ с множественным выбором
/// </summary>
public class PostUpdateMultipleAnswerRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"></param>
    public PostUpdateMultipleAnswerRequest(PostUpdateMultipleAnswerRequest request)
    {
        TestAttemptId = request.TestAttemptId;
        QuestionId = request.QuestionId;
        ChoicesId = request.ChoicesId;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostUpdateMultipleAnswerRequest()
    {
    }
    
    /// <summary>
    /// Id попытки
    /// </summary>
    public int TestAttemptId { get; set; }
    
    /// <summary>
    /// Id вопроса
    /// </summary>
    public int QuestionId { get; set; }

    /// <summary>
    /// Id выбора
    /// </summary>
    public List<int> ChoicesId { get; set; } = new();
}