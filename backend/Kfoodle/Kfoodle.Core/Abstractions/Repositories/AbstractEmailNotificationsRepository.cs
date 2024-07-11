using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;

namespace Kfoodle.Core.Abstractions.Repositories;


/// <summary>
/// Репозиторий для уведомлений
/// </summary>
/// <param name="dbContext">Контекст базы данных</param>
public abstract class AbstractEmailNotificationsRepository(IDbContext dbContext)
    : GenericRepository<EmailNotification, Guid>(dbContext)
{
    /// <summary>
    /// Получить неотправленные уведомления
    /// </summary>
    /// <param name="takeCount">Кол-во</param>
    /// <returns>Уведомления</returns>
    public abstract Task<List<EmailNotification>> GetNotSentNotifications(int takeCount);
}