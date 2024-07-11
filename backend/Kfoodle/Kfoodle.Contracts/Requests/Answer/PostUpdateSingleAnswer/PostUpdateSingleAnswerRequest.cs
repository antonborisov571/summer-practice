namespace Kfoodle.Contracts.Requests.Answer.PostUpdateSingleAnswer;

/// <summary>
/// Ответ для задания с единственным ответом
/// </summary>
public class PostUpdateSingleAnswerRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"></param>
    public PostUpdateSingleAnswerRequest(PostUpdateSingleAnswerRequest request)
    {
        TestAttemptId = request.TestAttemptId;
        QuestionId = request.QuestionId;
        ChoiceId = request.ChoiceId;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostUpdateSingleAnswerRequest()
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
    public int ChoiceId { get; set; }
}