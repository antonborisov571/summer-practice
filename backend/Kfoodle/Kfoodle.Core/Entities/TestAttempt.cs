namespace Kfoodle.Core.Entities;

/// <summary>
/// Попытка
/// </summary>
public class TestAttempt
{
    /// <summary>
    /// Id попытки
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public string UserId { get; set; } = null!;

    /// <summary>
    /// Пользователь
    /// </summary>
    public User User { get; set; } = null!;
    
    /// <summary>
    /// Id теста
    /// </summary>
    public Guid TestId { get; set; }

    /// <summary>
    /// Тест
    /// </summary>
    public Test Test { get; set; } = null!;

    /// <summary>
    /// Начало попытки
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Конец попытки
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Балл
    /// </summary>
    public int Score { get; set; }
    
    /// <summary>
    /// Число правильных ответов
    /// </summary>
    public int NumCorrectAnswers { get; set; }

    /// <summary>
    /// Ответы
    /// </summary>
    public List<Answer> Answers { get; set; } = new();
}