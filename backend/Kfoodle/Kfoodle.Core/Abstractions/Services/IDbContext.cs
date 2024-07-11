using Kfoodle.Core.Entities;
using Microsoft.EntityFrameworkCore;
using File = Kfoodle.Core.Entities.File;

namespace Kfoodle.Core.Abstractions.Services;

/// <summary>
/// Интерфейс контекста бд
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Уведомления на почту
    /// </summary>
    public DbSet<EmailNotification> EmailNotifications { get; set; }
    
    /// <summary>
    /// Тесты
    /// </summary>
    public DbSet<Test> Tests { get; set; }
    
    /// <summary>
    /// Вопросы
    /// </summary>
    public DbSet<Question> Questions { get; set; }
    
    /// <summary>
    /// Варианты
    /// </summary>
    public DbSet<Choice> Choices { get; set; }
    
    /// <summary>
    /// Попытки
    /// </summary>
    public DbSet<TestAttempt> TestAttempts { get; set; }
    
    /// <summary>
    /// Ответы
    /// </summary>
    public DbSet<Answer> Answers { get; set; }
    
    /// <summary>
    /// Файлы
    /// </summary>
    public DbSet<File> Files { get; set; }
    
    /// <inheritdoc cref="DbContext.Set{TEntity}()"/>
    public DbSet<T> Set<T>() where T: class;
    
    /// <inheritdoc cref="DbContext.SaveChangesAsync(System.Threading.CancellationToken)"/>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}