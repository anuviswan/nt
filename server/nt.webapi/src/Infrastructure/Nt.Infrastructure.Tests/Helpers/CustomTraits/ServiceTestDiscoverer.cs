using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Helpers.CustomTraits;
public class ServiceTestDiscoverer : ITraitDiscoverer
{
    public const string TypeName = $"{TraitDiscovererBase.AssemblyName}.Helpers.CustomTraits.ServiceTestDiscoverer";
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new ("Group", "Service");
        var name = traitAttribute.GetNamedArgument<string>("Name");
        if (!string.IsNullOrEmpty(name))
        {
            yield return new ("Service", name);
        }
    }
}
