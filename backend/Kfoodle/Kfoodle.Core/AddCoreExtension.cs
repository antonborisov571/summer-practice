﻿using System.Reflection;
using Kfoodle.Core.Abstractions;
using Kfoodle.Core.Abstractions.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Kfoodle.Core.Common.Behaviors;
using Kfoodle.Core.Models;
using Kfoodle.Core.Profiles;
using Kfoodle.Core.Services;

namespace Kfoodle.Core;

/// <summary>
/// Добавление Core слоя(Инъекция всех зависимостей Core)
/// </summary>
public static class AddCoreLayoutExtension
{
    /// <summary>
    /// Добавление сервисов в коллекцию
    /// </summary>
    /// <param name="services">Builder.Services</param>
    /// <param name="options">Настройки для API КЛАДР</param>
    /// <returns>Коллекцию сервисов с добавленными зависимостями</returns>
    public static IServiceCollection AddCoreLayout(this IServiceCollection services, SearchCityOptions options)
    {
        services.AddMediatR(config 
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddSingleton(options);
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorForRequest<,>));
        services.AddAutoMapper(configurationExpression => configurationExpression.AddProfile<AppMappingProfile>());
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserClaimsManager, UserClaimsManager>();
        services.AddScoped<IAvatarService, AvatarService>();
        services.AddHttpClient();

        return services;
    }
}