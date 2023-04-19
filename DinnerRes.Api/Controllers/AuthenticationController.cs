using DinnerRes.Api.Contracts.Authentication;
using DinnerRes.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using DinnerRes.Application.Authentication.Interfaces;

namespace DinnerRes.Api.Controllers;

[Route("auth")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }
    
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = _authService.Register
            (request.FirstName, request.LastName, request.Email, request.Password);

        return result.Match(
            authenticationResult => Ok(MapResultToResponse(authenticationResult)),
            errors => Problem(errors)
        );
    }
    
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = _authService.Login(request.Email, request.Password);

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