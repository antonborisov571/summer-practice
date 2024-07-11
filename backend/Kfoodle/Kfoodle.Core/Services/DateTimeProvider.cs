using Kfoodle.Core.Abstractions.Services;

namespace Kfoodle.Core.Services;

/// <summary>
/// Провайдер дат
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime CurrentDate => DateTime.UtcNow;
}