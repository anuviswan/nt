namespace UserService.Api.Settings;

public record BlobConfig
{
    public string ConnectionString { get; set; } = null!;
}
