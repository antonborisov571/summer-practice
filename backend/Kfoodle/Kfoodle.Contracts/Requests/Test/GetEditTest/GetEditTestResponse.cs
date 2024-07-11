namespace Kfoodle.Contracts.Requests.Test.GetEditTest;

/// <summary>
/// Ответ на запрос о изменении теста
/// </summary>
public class GetEditTestResponse
{
    /// <summary>
    /// Id
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
    /// Время в минутах
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
}