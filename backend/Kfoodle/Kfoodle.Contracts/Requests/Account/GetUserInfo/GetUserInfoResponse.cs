namespace Kfoodle.Contracts.Requests.Account.GetUserInfo;

/// <summary>
/// Ответ для запроса на получение информации о пользователе
/// </summary>
public class GetUserInfoResponse
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = default!;
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Дата рождения пользователя 
    /// </summary>
    public DateTime Birthday { get; set; } = default!;
    
    /// <summary>
    /// Дата регистрации пользователя
    /// </summary>
    public DateTime DateRegistration { get; set; } = default!;
    
    /// <summary>
    /// Ссылка на аватар пользователя
    /// </summary>
    public string? Avatar { get; set; } = default!;

    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; } = default!;
    
    /// <summary>
    /// Включена ли двухфакторка
    /// </summary>
    public bool TwoFactorEnabled { get; set; }
}