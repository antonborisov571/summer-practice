using Microsoft.Extensions.DependencyInjection;
using Kfoodle.Core.Abstractions;
using Kfoodle.Core.Abstractions.Repositories;
using Kfoodle.Core.Abstractions.Services;
using Kfoodle.Data.PostgreSQL.Repositories;

namespace Kfoodle.Data.PostgreSQL;

/// <summary>
/// Входная точка
/// </summary>
public static class Entry
{
    /// <summary>
    /// Регистрация зависимостей
    /// </summary>
    public static void AddPostgreSqlLayout(this IServiceCollection serviceCollection)
    {
        if (serviceCollection is null)
            throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Entry).Assembly));
        serviceCollection.AddDbContext<AppDbContext>();
        serviceCollection.AddScoped<IDbContext, AppDbContext>();
        serviceCollection.AddTransient<Migrator>();
        serviceCollection.AddScoped<AbstractFilesRepository, FilesRepository>();
        serviceCollection.AddScoped<AbstractEmailNotificationsRepository, EmailNotificationsRepository>();
        serviceCollection.AddScoped<AbstractTestRepository, TestRepository>();
        serviceCollection.AddLogging();
    } 
}