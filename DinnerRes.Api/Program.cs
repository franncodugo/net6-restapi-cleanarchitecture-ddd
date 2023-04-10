using System.ComponentModel;
using DinnerRes.Api.Filters;
using DinnerRes.Api.Middleware;
using DinnerRes.Application;
using DinnerRes.Application.Authentication;
using DinnerRes.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddControllers( opt => opt.Filters.Add<ErrorHandlingFilterAttribute>());
}

var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

