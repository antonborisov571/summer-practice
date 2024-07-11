using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Kfoodle.Core.Entities;

/// <summary>
/// Сущность пользователя
/// </summary>
public class User : IdentityUser, IEntity<string>
{
    /// <summary>
    /// JWT
    /// </summary>
    public string? AccessToken { get; set; }
    
    /// <summary>
    /// Токен для обновления JWT
    /// </summary>
    public string? RefreshToken { get; set; }
    
    /// <summary>
    /// Время жизни Refresh Token
    /// </summary>
    public DateTime? RefreshTokenExpiryTime { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// Ссылка на аватар пользователя
    /// </summary>
    [Url]
    public string? Avatar { get; set; } 
    
    /// <summary>
    /// День рождения пользователя
    /// </summary>
    public DateTime Birthday { get; set; }
    
    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime DateRegistration { get; set; }
    
    /// <summary>
    /// Телефон пользователя
    /// </summary>
    public string? Phone { get; protected set; }

    /// <summary>
    /// Подтвержден
    /// </summary>
    public bool IsConfirmed { get; protected set; }

    /// <summary>
    /// Тесты от пользователя
    /// </summary>
    public List<Test> AuthorTests { get; set; } = new();

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="id">Ид пользователя</param>
    /// <param name="login">Логин пользователя</param>
    /// <param name="birthday">Дата рождения</param>
    /// <param name="email">E-mail пользователя</param>
    /// <param name="phone">Телефон</param>
    /// <param name="passwordHash">Хеш пароля</param>
    /// <returns></returns>
    [Obsolete("Только для тестов")]
    public static User CreateForTest(
        Guid? id = default,
        string login = default!,
        DateTime birthday = default,
        string email = default!,
        string phone = default!,
        string? passwordHash = default)
        => new()
        {
            Id = id.ToString() ?? Guid.NewGuid().ToString(),
            Birthday = birthday,
            Email = email,
            Phone = phone,
            PasswordHash = passwordHash
        };
}