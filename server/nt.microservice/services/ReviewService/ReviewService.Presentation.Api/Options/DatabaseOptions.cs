namespace ReviewService.Presenation.Api.Options;

public class DatabaseOptions
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ReviewCollectionName { get; set; } = null!;
}