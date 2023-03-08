using DinnerRes.Application.Authentication;
using DinnerRes.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DinnerRes.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }
    
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = _authService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        var response = new AuthenticationResponse(result.Id, result.FirstName, result.LastName, result.Email, result.Token);
        return Ok(response);
    }
    
    [HttpPost("login")]
    public IActionResult Register(LoginRequest request)
    {
        var result = _authService.Login(request.Email, request.Password);
        var response = new AuthenticationResponse(result.Id, result.FirstName, result.LastName, result.Email, result.Token);
        return Ok(response);
    }
}