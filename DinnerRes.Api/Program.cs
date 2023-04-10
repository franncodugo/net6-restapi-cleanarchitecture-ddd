using System.ComponentModel;
using DinnerRes.Api.Errors;
using DinnerRes.Api.Filters;
using DinnerRes.Api.Middleware;
using DinnerRes.Application;
using DinnerRes.Application.Authentication;
using DinnerRes.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    //builder.Services.AddControllers( opt => opt.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();

    builder.Services.AddSingleton<ProblemDetailsFactory, DinnerResProblemDetailsFactory>();
}

var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

