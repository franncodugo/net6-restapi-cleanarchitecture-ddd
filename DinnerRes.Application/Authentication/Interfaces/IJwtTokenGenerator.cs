namespace DinnerRes.Application.Authentication.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Domain.Entities.User user);
}