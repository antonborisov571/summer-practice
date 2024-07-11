using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;

namespace Kfoodle.Data.PostgreSQL.Repositories;

/// <inheritdoc />
public class FilesRepository(IDbContext dbContext) : AbstractFilesRepository(dbContext);