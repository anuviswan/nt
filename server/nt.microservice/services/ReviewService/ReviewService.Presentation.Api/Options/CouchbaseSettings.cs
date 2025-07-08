namespace ReviewService.Presenation.Api.Options;

public record CouchbaseSettings
{
    public string ConnectionString { get; init; } = string.Empty;
    public string BucketName { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

// EnsureBucketService.cs



