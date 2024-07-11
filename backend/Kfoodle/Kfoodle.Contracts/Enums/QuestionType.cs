namespace Kfoodle.Contracts.Enums;

/// <summary>
/// Тип вопроса
/// </summary>
public enum QuestionType
{
    /// <summary>
    /// Задание с одиночным ответом
    /// </summary>
    SingleAnswer,
    /// <summary>
    /// Задание с множественным выбором
    /// </summary>
    MultipleAnswers,
    /// <summary>
    /// Задание с вводом ответа
    /// </summary>
    InputAnswer,
    /// <summary>
    /// Задание с последовательностью
    /// </summary>
    SequenceAnswer
}