namespace UserIdentityAggregatorService.Api.Settings;

public record ServiceDiscovery
{
    public List<string> Services { get; set; } = new List<string>();
    public string ResolverName { get; set; } = null!;
    public string ResolverPort { get; set; } = null!;
}
