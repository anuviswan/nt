namespace UserService.Api.Settings;

public record RabbitMqSettings
{
    public string Host { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;    
    public string Password { get; init; } = string.Empty;
}
