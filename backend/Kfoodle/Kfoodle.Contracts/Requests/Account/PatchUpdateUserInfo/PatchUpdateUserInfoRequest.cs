namespace Kfoodle.Contracts.Requests.Account.PatchUpdateUserInfo;

/// <summary>
/// Запрос на обновление данных о пользователе
/// </summary>
public class PatchUpdateUserInfoRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">PatchUpdateUserInfoRequest</param>
    public PatchUpdateUserInfoRequest(PatchUpdateUserInfoRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        FirstName = request.FirstName;
        LastName = request.LastName;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PatchUpdateUserInfoRequest()
    {
    } 
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? LastName { get; set; }
}