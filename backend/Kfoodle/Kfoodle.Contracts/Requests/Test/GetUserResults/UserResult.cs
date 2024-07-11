namespace Kfoodle.Contracts.Requests.Test.GetUserResults;

/// <summary>
/// Результат пользователя
/// </summary>
public class UserResult
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = null!;
    
    /// <summary>
    /// Балл
    /// </summary>
    public int Score { get; set; }
    
    /// <summary>
    /// Начало тестирования
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Конец тестирования
    /// </summary>
    public DateTime EndTime { get; set; }
}