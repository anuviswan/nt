namespace nt.saga.orchestrator.test.Configurations;

internal record ApiSetting 
{
    public string Key { get; set; }
    public string Uri { get; set; }
    public string FileName { get; set; }
    public string ClassName { get; set; }
    public string Namespace { get; set; }
}
