namespace Kfoodle.Contracts.Requests.Test.GetUserResults;

/// <summary>
/// Ответ на запрос о получения результатов пользователей
/// </summary>
public class GetUserResultsResponse
{
    /// <summary>
    /// Наименование теста
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Результаты пользователей
    /// </summary>
    public List<UserResult> UserResults { get; set; } = new();
}