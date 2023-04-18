using DinnerRes.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DinnerRes.Api.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, "Unexpected Error")
        };
        
        return Problem(statusCode: statusCode, title: message);
    }
}