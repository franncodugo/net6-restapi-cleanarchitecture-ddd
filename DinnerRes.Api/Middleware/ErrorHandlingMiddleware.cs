using System.Net;
using System.Text.Json;

namespace DinnerRes.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public ErrorHandlingMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected
        var result = JsonSerializer.Serialize(new { error = "An error occurred while processing your request" });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}