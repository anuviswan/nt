namespace MovieService.Api.Settings;

public record LoggerConfiguration
{
    public string? FileName { get; set; }
    public LogLevel? LogLevel { get; set; } 
}
