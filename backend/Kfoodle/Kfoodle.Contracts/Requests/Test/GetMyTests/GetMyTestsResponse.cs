using Kfoodle.Contracts.Requests.Test.GetCatalog;

namespace Kfoodle.Contracts.Requests.Test.GetMyTests;

/// <summary>
/// Ответ на запрос о получении тестов
/// </summary>
public class GetMyTestsResponse
{
    /// <summary>
    /// Тесты
    /// </summary>
    public List<TestItem> TestItems { get; set; } = new();
}