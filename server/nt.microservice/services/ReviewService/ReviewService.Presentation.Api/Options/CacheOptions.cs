namespace ReviewService.Presenation.Api.Options;

public record CacheOptions
{
    public string? CacheName { get; init; } = "ReviewServiceCache"; // Default cache name
    public int ExpirationInMinutes { get; init; } = 60; // Default expiration time in minutes
    public bool EnableCaching { get; init; } = true; // Flag to enable or disable caching
    public string? ConnectionString { get; init; } = null; // Optional connection string for distributed cache
}
