namespace Kfoodle.Contracts.Requests.Test.GetEditQuestions;

/// <summary>
/// Ответ на запрос о изменении вопросов
/// </summary>
public class GetEditQuestionsResponse
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название теста
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание теста
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Дата окончания теста
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Время в минутах
    /// </summary>
    public int? Duration { get; set; } 

    /// <summary>
    /// Кол-во вопросов
    /// </summary>
    public int? NumQuestions { get; set; }
    
    /// <summary>
    /// Макс. число попыток
    /// </summary>
    public int? MaxAttempts { get; set; }

    /// <summary>
    /// Вопросы
    /// </summary>
    public List<Question> Questions { get; set; } = new();
}

