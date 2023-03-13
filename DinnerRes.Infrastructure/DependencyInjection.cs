using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace DinnerRes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}