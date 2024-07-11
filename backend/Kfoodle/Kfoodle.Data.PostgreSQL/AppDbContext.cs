using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Kfoodle.Data.PostgreSQL.Configurations;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Core.Entities;
using File = Kfoodle.Core.Entities.File;

namespace Kfoodle.Data.PostgreSQL;

/// <summary>
/// Контекст БД
/// </summary>
public class AppDbContext
    : IdentityDbContext<User>, IDbContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public AppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    /// <inheritdoc cref="EmailNotification"/>
    public DbSet<EmailNotification> EmailNotifications { get; set; }
    
    /// <inheritdoc cref="File"/>
    public DbSet<File> Files { get; set; }
    
    public DbSet<Test> Tests { get; set; }
    
    public DbSet<Question> Questions { get; set; }
    
    public DbSet<Choice> Choices { get; set; }
    
    public DbSet<TestAttempt> TestAttempts { get; set; }
    
    public DbSet<Answer> Answers { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EmailNotificationConfiguration());
        builder.ApplyConfiguration(new FileConfiguration());
        builder.ApplyConfiguration(new TestConfiguration());
        builder.ApplyConfiguration(new QuestionConfiguration());
        builder.ApplyConfiguration(new ChoiceConfiguration());
        builder.ApplyConfiguration(new TestAttemptConfiguration());
        builder.ApplyConfiguration(new AnswerConfiguration());
        
        base.OnModelCreating(builder);
    }
}