namespace UserService.Api.Settings;

public record RabbitMqSettings
{
    public string Host { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;    
    public string Password { get; init; } = string.Empty;
}

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