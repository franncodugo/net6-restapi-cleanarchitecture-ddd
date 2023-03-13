namespace DinnerRes.Api.Contracts.Authentication;

public record LoginRequest
(
    string Email,
    string Password
);