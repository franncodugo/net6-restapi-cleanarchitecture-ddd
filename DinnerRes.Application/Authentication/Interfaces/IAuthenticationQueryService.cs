using ErrorOr;
namespace DinnerRes.Application.Authentication.Interfaces;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}