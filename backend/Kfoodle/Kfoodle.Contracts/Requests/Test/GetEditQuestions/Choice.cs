namespace Kfoodle.Contracts.Requests.Test.GetEditQuestions;

/// <summary>
/// Выбор
/// </summary>
public class Choice
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id вопроса
    /// </summary>
    public int QuestionId { get; set; }

    /// <summary>
    /// Текст варианта
    /// </summary>
    public string ChoiceText { get; set; } = null!;

    /// <summary>
    /// Правильный ли вариант
    /// </summary>
    public bool IsCorrect { get; set; }
}