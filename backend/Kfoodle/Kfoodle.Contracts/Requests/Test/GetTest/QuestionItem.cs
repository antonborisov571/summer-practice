using Kfoodle.Contracts.Enums;

namespace Kfoodle.Contracts.Requests.Test.GetTest;

/// <summary>
/// Вопрос
/// </summary>
public class QuestionItem
{
    /// <summary>
    /// Id вопроса
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Текст вопроса
    /// </summary>
    public string QuestionText { get; set; } = null!;
    
    /// <summary>
    /// Тип вопроса
    /// </summary>
    public QuestionType QuestionType { get; set; }
    
    /// <summary>
    /// Ответ
    /// </summary>
    public string? Answer { get; set; }

    /// <summary>
    /// Варианты
    /// </summary>
    public List<ChoiceItem>? Choices { get; set; } = new();
}