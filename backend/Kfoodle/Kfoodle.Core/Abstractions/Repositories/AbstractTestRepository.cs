using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;

namespace Kfoodle.Core.Abstractions.Repositories;

/// <summary>
/// Репозиторий для тестов
/// </summary>
/// <param name="dbContext">Контекст базы данных</param>
public abstract class AbstractTestRepository(IDbContext dbContext)
    : GenericRepository<Test, Guid>(dbContext)
{
    /// <summary>
    /// Получить тест
    /// </summary>
    /// <param name="id">Id теста</param>
    /// <returns></returns>
    public abstract Task<Test?> GetTest(Guid id);

    /// <summary>
    /// Получить тест с вопросами
    /// </summary>
    /// <param name="id">Id теста</param>
    /// <returns></returns>
    public abstract Task<Test?> GetTestWithQuestions(Guid id);
}