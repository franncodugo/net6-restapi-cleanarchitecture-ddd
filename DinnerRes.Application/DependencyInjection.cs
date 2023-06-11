using DinnerRes.Application.Authentication.Commands;
using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Application.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DinnerRes.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        
        return services;
    }
}