using DinnerRes.Api.Contracts.Authentication;
using DinnerRes.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Application.Authentication.Queries;

namespace DinnerRes.Api.Controllers;

[Route("auth")]
public class AuthenticationController : BaseApiController
{
    private readonly IAuthenticationCommandService _authCommandService;
    private readonly IAuthenticationQueryService _authQueriesService;

    public AuthenticationController(IAuthenticationCommandService authCommandService, 
        IAuthenticationQueryService authQueriesService)
    {
        _authCommandService = authCommandService ?? throw new ArgumentNullException(nameof(authCommandService));
        _authQueriesService = authQueriesService ?? throw new ArgumentNullException(nameof(authQueriesService));
    }
    
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = _authCommandService.Register
            (request.FirstName, request.LastName, request.Email, request.Password);

        return result.Match(
            authenticationResult => Ok(MapResultToResponse(authenticationResult)),
            errors => Problem(errors)
        );
    }
    
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = _authQueriesService.Login(request.Email, request.Password);

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