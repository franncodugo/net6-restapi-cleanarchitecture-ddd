using DinnerRes.Api.Contracts.Authentication;
using DinnerRes.Application.Authentication;
using DinnerRes.Application.Authentication.Commands.Register;
using Microsoft.AspNetCore.Mvc;
using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Application.Authentication.Queries;
using DinnerRes.Application.Authentication.Queries.Login;
using MediatR;

namespace DinnerRes.Api.Controllers;

[Route("auth")]
public class AuthenticationController : BaseApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand
            (request.FirstName, request.LastName, request.Email, request.Password);

        var result = await _mediator.Send(command);
        
        return result.Match(
            authenticationResult => Ok(MapResultToResponse(authenticationResult)),
            errors => Problem(errors)
        );
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var result = await _mediator.Send(query);
        
        return result.Match(
            authResult => Ok(MapResultToResponse(authResult)),
            errors => Problem(errors)
        );
    }
    
    private static AuthenticationResponse MapResultToResponse(AuthenticationResult result)
    {
        return new AuthenticationResponse(
            result.user.Id, 
            result.user.FirstName, 
            result.user.LastName, 
            result.user.Email, 
            result.Token);
    }
}