using ErrorOr;

namespace DinnerRes.Application.Authentication.Interfaces;

public interface IAuthenticationCommandService
{
    ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
    
}