using DinnerRes.Application.Authentication.Interfaces;
using DinnerRes.Application.User.Interfaces;
using DinnerRes.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace DinnerRes.Application.Authentication.Queries.Login;

public sealed class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // Validate the given User exists
        if (_userRepository.GetUserByEmail(query.Email) is not { } user)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // Validate the password is ok
        if (user.Password != query.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult
            (user, token);
    }
}