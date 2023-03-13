using DinnerRes.Application.Authentication.Interfaces;

namespace DinnerRes.Application.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
    }
    
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // TODO: Check if User already exists.
        
        // TODO: Create User.
        
        // TODO: Create JWT Token.
        Guid userId = Guid.NewGuid();

        var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        
        return new AuthenticationResult
            (Guid.NewGuid(), firstName, lastName, email, token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult
            (Guid.NewGuid(), "firstName", "lastName", email, "token");    
    }
}