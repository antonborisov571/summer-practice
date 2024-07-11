namespace Kfoodle.Contracts.Requests.Test.GetPassedTests;

/// <summary>
/// Ответ на запрос о получении пройденных тестов
/// </summary>
public class GetPassedTestsResponse
{
    /// <summary>
    /// Результаты теста
    /// </summary>
    public List<TestResult> TestResults { get; set; } = new();
}