using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Data.PostgreSQL.Repositories;

/// <inheritdoc />
public class TestRepository(IDbContext dbContext) : AbstractTestRepository(dbContext)
{
    /// <inheritdoc />
    public override async Task<Test?> GetTest(Guid id)
    {
        return await dbContext.Tests
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <inheritdoc />
    public override async Task<Test?> GetTestWithQuestions(Guid id)
    {
        return await dbContext.Tests
            .Include(x => x.Author)
            .Include(x => x.Questions)
            .ThenInclude(x => x.Choices)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}