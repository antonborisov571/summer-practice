namespace Kfoodle.Contracts.Requests.Answer.PostUpdateInputAnswer;

/// <summary>
/// Запрос для обновления ответа
/// </summary>
public class PostUpdateInputAnswerRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"></param>
    public PostUpdateInputAnswerRequest(PostUpdateInputAnswerRequest request)
    {
        TestAttemptId = request.TestAttemptId;
        QuestionId = request.QuestionId;
        Answer = request.Answer;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostUpdateInputAnswerRequest()
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
    /// Ответ
    /// </summary>
    public string Answer { get; set; } = default!;
}