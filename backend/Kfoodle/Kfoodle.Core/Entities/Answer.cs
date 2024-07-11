namespace Kfoodle.Core.Entities;

/// <summary>
/// Сущность ответа
/// </summary>
public class Answer
{
    /// <summary>
    /// Id ответа
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Id попытки
    /// </summary>
    public int TestAttemptId { get; set; }

    /// <summary>
    /// Попытка
    /// </summary>
    public TestAttempt TestAttempt { get; set; } = null!;
    
    /// <summary>
    /// Id вопроса
    /// </summary>
    public int QuestionId { get; set; }

    /// <summary>
    /// Вопрос
    /// </summary>
    public Question Question { get; set; } = null!;
    
    /// <summary>
    /// Ответ
    /// </summary>
    public string? InputAnswer { get; set; }
    
    /// <summary>
    /// Выбранный вариант ответа
    /// </summary>
    public Choice? SingleChoice { get; set; }
    
    /// <summary>
    /// Список выбранных вариантов ответа
    /// </summary>
    public List<int> ChoicesId { get; set; } = new();
}