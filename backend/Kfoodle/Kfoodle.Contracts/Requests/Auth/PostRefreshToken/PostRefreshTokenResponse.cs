namespace Kfoodle.Contracts.Requests.Account.PostRefreshToken;

/// <summary>
/// Ответ на запрос об обновлении токена
/// </summary>
public class PostRefreshTokenResponse
{
    /// <summary>
    /// JWT
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// Токен для обновления JWT
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}