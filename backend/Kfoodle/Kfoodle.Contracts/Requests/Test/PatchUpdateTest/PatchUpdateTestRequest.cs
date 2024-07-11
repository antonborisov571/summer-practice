namespace Kfoodle.Contracts.Requests.Test.PatchUpdateTest;

/// <summary>
/// Запрос об обновлении теста
/// </summary>
public class PatchUpdateTestRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"></param>
    public PatchUpdateTestRequest(PatchUpdateTestRequest request)
    {
        Id = request.Id;
        Title = request.Title;
        Description = request.Description;
        Duration = request.Duration;
        EndDate = request.EndDate;
        NumQuestions = request.NumQuestions;
        MaxAttempts = request.MaxAttempts;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PatchUpdateTestRequest()
    {
    }
    
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название теста
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
}