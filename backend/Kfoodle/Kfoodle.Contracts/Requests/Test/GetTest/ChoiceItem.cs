namespace Kfoodle.Contracts.Requests.Test.GetTest;

/// <summary>
/// Выбор
/// </summary>
public class ChoiceItem
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
    /// Текст варианта
    /// </summary>
    public string ChoiceText { get; set; } = null!;

    /// <summary>
    /// Выбрали или не выбран
    /// </summary>
    public bool IsSelected { get; set; }
}