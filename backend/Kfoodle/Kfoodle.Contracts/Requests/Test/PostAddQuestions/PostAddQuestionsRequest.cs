namespace Kfoodle.Contracts.Requests.Test.PostAddQuestions;

/// <summary>
/// Запрос на добавление вопросов из другого теста
/// </summary>
public class PostAddQuestionsRequest
{
    /// <summary>
    /// Id другого теста
    /// </summary>
    public Guid SourceTestId { get; set; }
}