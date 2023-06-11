using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Application.User.Interfaces;
using DinnerRes.Domain.Common.Errors;
using MediatR;
using ErrorOr;

namespace DinnerRes.Application.Authentication.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, 
        CancellationToken cancellationToken)
    {
        // Check if User already exists.
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }
        
        // Create User.
        var userEntity = new Domain.Entities.User()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };
        
        _userRepository.Add(userEntity);
        
        // Create JWT Token.
        var token = _jwtTokenGenerator.GenerateToken(userEntity);
        
        return new AuthenticationResult
            (userEntity, token);
    }
}