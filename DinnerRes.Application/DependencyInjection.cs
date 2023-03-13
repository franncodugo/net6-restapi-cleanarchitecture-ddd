using DinnerRes.Application.Authentication;
using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Application.Authentication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DinnerRes.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        
        return services;
    }
}