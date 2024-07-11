namespace Kfoodle.Core.Entities;

/// <summary>
/// Тест
/// </summary>
public class Test : IEntity<Guid>
{
    /// <summary>
    /// Id теста
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название 
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; } 

    /// <summary>
    /// Дата окончания
    /// </summary>
    public DateTime? EndDate { get; set; }

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
    /// Id автора
    /// </summary>
    public string AuthorId { get; set; } = null!;

    /// <summary>
    /// Автор
    /// </summary>
    public User Author { get; set; } = null!;

    /// <summary>
    /// Вопросы
    /// </summary>
    public List<Question> Questions { get; set; } = new();

    /// <summary>
    /// Попытки
    /// </summary>
    public List<TestAttempt> TestAttempts { get; set; } = new();
}