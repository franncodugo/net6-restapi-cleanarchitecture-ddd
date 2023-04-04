using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Application.Common.Interfaces;
using DinnerRes.Application.User.Interfaces;
using DinnerRes.Infrastructure.Authentication;
using DinnerRes.Infrastructure.Common;
using DinnerRes.Infrastructure.User;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DinnerRes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration
        )
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));     
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}