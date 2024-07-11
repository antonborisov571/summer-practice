using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Kfoodle.WEB.Configurations;

/// <summary>
/// Конфигурация аутентификации
/// </summary>
public static class ConfigureAuthenticationExtension
{
    /// <summary>
    /// Добавление Аутентификации с настройкой JwtBearer
    /// </summary>
    /// <param name="services">Коллекция сервисов билдера</param>
    /// <param name="configuration">конфигурация(appsettings.json)</param>
    /// <returns>AuthenticationBuilder</returns>
    public static AuthenticationBuilder AddAuthenticationWithJwtAndExternalServices(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidateLifetime = true,
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
            };
        }).AddYandex(config =>
        {
            config.ClientId = configuration["Authentication:Yandex:AppId"]!;
            config.ClientSecret = configuration["Authentication:Yandex:AppSecret"]!;
        }).AddVkontakte(config =>
        {
            config.ApiVersion = configuration["Authentication:VK:VkApiVersion"]!;
            config.ClientId = configuration["Authentication:VK:AppId"]!;
            config.ClientSecret = configuration["Authentication:VK:AppSecret"]!;
            config.Scope.Add("email");
            config.Scope.Add("first_name");
            config.Scope.Add("last_name");
            config.Scope.Add("bdate");
        });
}