namespace Kfoodle.Contracts.Requests.Test.GetCatalog;

/// <summary>
/// Ответ на запрос о получении доступных тестов
/// </summary>
public class GetCatalogResponse
{
    /// <summary>
    /// Тесты 
    /// </summary>
    public List<TestItem> TestItems { get; set; } = new();
}