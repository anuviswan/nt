using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Helpers.CustomTraits;
public class ControllerTestDiscoverer : ITraitDiscoverer
{
    public const string TypeName = $"{TraitDiscovererBase.AssemblyName}.Helpers.CustomTraits.ControllerTestDiscoverer";
    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new ("Group", "Controller");
        var name = traitAttribute.GetNamedArgument<string>("Name");
        if (!string.IsNullOrEmpty(name))
        {
            yield return new ("Controller", name);
        }
    }
}
