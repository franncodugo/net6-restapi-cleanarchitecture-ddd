namespace DinnerRes.Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Secret { get; init; }
    public int ExpiresMinutes { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }
}