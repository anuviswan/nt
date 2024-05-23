namespace UserService.Api.Settings;

public record JwtSettings
{
    public string Key { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Aud { get; init; } = string.Empty;

    public bool Validate()
    {
        return Key is not null &&
            Issuer is not null &&
            Aud is not null;
    }
}