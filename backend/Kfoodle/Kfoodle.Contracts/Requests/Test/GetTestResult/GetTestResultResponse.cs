namespace Kfoodle.Contracts.Requests.Test.GetTestResult;

/// <summary>
/// Ответ на запрос о получении результатов
/// </summary>
public class GetTestResultResponse 
{
    /// <summary>
    /// Балл
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Название теста
    /// </summary>
    public string Title { get; set; } = null!;
    
    /// <summary>
    /// Число вопросов
    /// </summary>
    public int NumQuestions { get; set; }
    
    /// <summary>
    /// Число правильных ответов
    /// </summary>
    public int NumCorrectAnswers { get; set; }
    
    /// <summary>
    /// Начало тестирования
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Конец тестирования
    /// </summary>
    public DateTime EndTime { get; set; }
}