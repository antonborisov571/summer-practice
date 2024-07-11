using Kfoodle.Contracts.Requests.Test.GetTest;

namespace Kfoodle.Contracts.Requests.Test.GetStartTest;

/// <summary>
/// Ответ на запрос о начале теста
/// </summary>
public class GetStartTestResponse
{
    /// <summary>
    /// Id теста
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Доступ к тесту
    /// </summary>
    public bool IsAccess { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Окончание теста
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// Начало теста
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Время в минутах на прохождение
    /// </summary>
    public int? Duration { get; set; } 

    /// <summary>
    /// Число вопросов
    /// </summary>
    public int? NumQuestions { get; set; }
    
    /// <summary>
    /// Число попыток
    /// </summary>
    public int? MaxAttempts { get; set; }
    
    /// <summary>
    /// Id попытки
    /// </summary>
    public int TestAttemptId { get; set; }

    /// <summary>
    /// Вопросы
    /// </summary>
    public List<QuestionItem> Questions { get; set; } = new();
}