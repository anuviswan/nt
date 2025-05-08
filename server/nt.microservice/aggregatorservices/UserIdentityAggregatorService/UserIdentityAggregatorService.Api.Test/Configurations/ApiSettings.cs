namespace UserIdentityAggregatorService.Api.Test.Configurations;

internal record ApiSetting
{
    public required string Key { get; set; }
    public required string Uri { get; set; }
    public string? FileName { get; set; }
    public string? ClassName { get; set; }
    public string? Namespace { get; set; }
}
