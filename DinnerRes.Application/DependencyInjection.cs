using DinnerRes.Application.Authentication;
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