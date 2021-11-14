using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Helpers.CustomTraits;

[AttributeUsage(AttributeTargets.Method)]
[TraitDiscoverer(ServiceTestDiscoverer.TypeName, TraitDiscovererBase.AssemblyName)]
public class ServiceTestAttribute:Attribute,ITraitAttribute
{
    public string Name { get; set; }
    public ServiceTestAttribute(string name) => Name = name;

    public ServiceTestAttribute()
    {

    }
}
