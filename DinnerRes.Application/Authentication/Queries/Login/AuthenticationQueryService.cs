using ErrorOr;
using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Application.User.Interfaces;
using DinnerRes.Domain.Common.Errors;

namespace DinnerRes.Application.Authentication.Queries;

public sealed class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    
    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    
    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // Validate the given User exists
        if (_userRepository.GetUserByEmail(email) is not { } user)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // Validate the password is ok
        if (user.Password != password)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult
            (user, token);
    }
}