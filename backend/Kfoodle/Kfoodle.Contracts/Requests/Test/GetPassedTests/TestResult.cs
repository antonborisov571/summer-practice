namespace Kfoodle.Contracts.Requests.Test.GetPassedTests;

/// <summary>
/// Результаты теста
/// </summary>
public class TestResult
{
    /// <summary>
    /// Балл
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// Название
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
    /// Начало теста
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Конец теста
    /// </summary>
    public DateTime EndTime { get; set; }
}