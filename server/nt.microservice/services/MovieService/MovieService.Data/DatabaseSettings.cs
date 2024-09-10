namespace MovieService.Data;
public class DatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string MovieCollectionName { get; set; } = null!;
}
