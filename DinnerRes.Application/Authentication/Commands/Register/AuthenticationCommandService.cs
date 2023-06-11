using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Application.User.Interfaces;
using DinnerRes.Domain.Common.Errors;
using ErrorOr;

namespace DinnerRes.Application.Authentication.Commands;

public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        // Check if User already exists.
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        
        // Create User.
        var userEntity = new Domain.Entities.User()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        
        _userRepository.Add(userEntity);
        
        // Create JWT Token.
        var token = _jwtTokenGenerator.GenerateToken(userEntity);
        
        return new AuthenticationResult
            (userEntity, token);
    }
}