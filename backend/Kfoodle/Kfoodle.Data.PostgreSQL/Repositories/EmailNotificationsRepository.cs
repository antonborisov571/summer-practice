using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kfoodle.Data.PostgreSQL.Repositories;

/// <inheritdoc />
public class EmailNotificationsRepository(IDbContext dbContext) 
    : AbstractEmailNotificationsRepository(dbContext)
{
    private readonly IDbContext _dbContext = dbContext;

    /// <inheritdoc />
    public override async Task<List<EmailNotification>> GetNotSentNotifications(int takeCount)
    {
        return await _dbContext.EmailNotifications
            .Where(x => x.SentDate == null)
            .Take(takeCount)
            .ToListAsync();
        
    }
}