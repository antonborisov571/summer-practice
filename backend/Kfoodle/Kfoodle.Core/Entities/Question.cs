using Kfoodle.Contracts.Enums;
using Kfoodle.Core.Enums;

namespace Kfoodle.Core.Entities;

/// <summary>
/// Вопрос
/// </summary>
public class Question
{
    /// <summary>
    /// Id вопроса
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Id теста
    /// </summary>
    public Guid TestId { get; set; }

    /// <summary>
    /// Тест
    /// </summary>
    public Test Test { get; set; } = null!;

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