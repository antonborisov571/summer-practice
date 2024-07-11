namespace Kfoodle.Contracts.Requests.Test.GetCatalog;

/// <summary>
/// Тест
/// </summary>
public class TestItem
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Название теста
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание теста
    /// </summary>
    public string Description { get; set; } = null!;
}