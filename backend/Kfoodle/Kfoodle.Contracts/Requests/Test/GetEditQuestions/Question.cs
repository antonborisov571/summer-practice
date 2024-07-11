using Kfoodle.Contracts.Enums;

namespace Kfoodle.Contracts.Requests.Test.GetEditQuestions;

/// <summary>
/// Вопрос
/// </summary>
public class Question
{
    /// <summary>
    /// Id
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
    /// Правильный ответ
    /// </summary>
    public string? RightAnswer { get; set; }

    /// <summary>
    /// Варианты
    /// </summary>
    public List<Choice>? Choices { get; set; } = new();
}