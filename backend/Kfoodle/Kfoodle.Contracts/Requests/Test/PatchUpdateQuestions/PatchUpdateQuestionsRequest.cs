using Kfoodle.Contracts.Requests.Test.GetEditQuestions;

namespace Kfoodle.Contracts.Requests.Test.PatchUpdateQuestions;

/// <summary>
/// Запрос об обновлении вопросов
/// </summary>
public class PatchUpdateQuestionsRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"></param>
    public PatchUpdateQuestionsRequest(PatchUpdateQuestionsRequest request)
    {
        Id = request.Id;
        Title = request.Title;
        Description = request.Description;
        EndDate = request.EndDate;
        Duration = request.Duration;
        NumQuestions = request.NumQuestions;
        Questions = request.Questions;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PatchUpdateQuestionsRequest()
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

    /// <summary>
    /// Вопросы
    /// </summary>
    public List<Question> Questions { get; set; } = new();
}