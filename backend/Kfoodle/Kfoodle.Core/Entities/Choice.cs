namespace Kfoodle.Core.Entities;

/// <summary>
/// Выбор
/// </summary>
public class Choice
{
    /// <summary>
    /// Id выбора
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id вопроса
    /// </summary>
    public int QuestionId { get; set; }

    /// <summary>
    /// Вопрос
    /// </summary>
    public Question Question { get; set; } = null!;

    /// <summary>
    /// Текст варианта
    /// </summary>
    public string ChoiceText { get; set; } = null!;

    /// <summary>
    /// Правильный вариант ли ответа
    /// </summary>
    public bool IsCorrect { get; set; }
}