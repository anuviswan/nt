namespace nt.shared.dto.Configurations;

public record ServiceMappingConfig
{
    public List<Service> Services { get; set; } = [];
    public string RegistryUri { get; set; } = null!;
}

public record Service(string Key, string Name);
