using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Application.Common.Exceptions;
using DinnerRes.Application.User.Interfaces;

namespace DinnerRes.Application.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    
    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check if User already exists.
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new EmailDuplicatedException();
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

    public AuthenticationResult Login(string email, string password)
    {
        // Validate the given User exists
        if (_userRepository.GetUserByEmail(email) is not Domain.Entities.User user)
        {
            // TODO: Refactor this.
            throw new Exception("User with given email does not exist.");
        }
        
        // Validate the password is ok
        if (user.Password != password)
        {
            // TODO: Refactor this.
            throw new Exception("The given password is incorrect.");
        }
        
        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult
            (user, token);
    }
}