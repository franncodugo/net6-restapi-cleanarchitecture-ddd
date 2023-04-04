namespace DinnerRes.Application.Authentication;

public record AuthenticationResult
(
    Domain.Entities.User user,
    string Token
);