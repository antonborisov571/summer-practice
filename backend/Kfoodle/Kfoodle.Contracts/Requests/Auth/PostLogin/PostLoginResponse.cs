namespace Kfoodle.Contracts.Requests.Auth.PostLogin;

/// <summary>
/// Результат логина для PostLogin
/// </summary>
public class PostLoginResponse
{
    /// <summary>
    /// JWT
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// Токен для обновления JWT
    /// </summary>
    public string RefreshToken { get; set; } = default!;

    /// <summary>
    /// Включена ли двухфакторная авторизация
    /// </summary>
    public bool TwoFactorEnabled { get; set; } = default!;
}