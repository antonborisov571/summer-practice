using Kfoodle.Core.Abstractions.Services;
using File = Kfoodle.Core.Entities.File;

namespace Kfoodle.Core.Abstractions.Repositories;

/// <summary>
/// Репозиторий для файлов
/// </summary>
/// <param name="dbContext">Контекст базы данных</param>
public abstract class AbstractFilesRepository(IDbContext dbContext) 
    : GenericRepository<File, Guid>(dbContext);