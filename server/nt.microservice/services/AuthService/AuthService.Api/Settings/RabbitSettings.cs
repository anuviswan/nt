namespace AuthService.Api.Settings;

public record RabbitMqSettings
{
    public string Uri { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;

    public bool Validate()
    {
        return Uri is not null
            && UserName is not null
            && Password is not null;
    }
}


