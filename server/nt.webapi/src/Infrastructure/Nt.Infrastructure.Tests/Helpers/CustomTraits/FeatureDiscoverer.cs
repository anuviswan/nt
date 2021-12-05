using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Helpers.CustomTraits;
public class FeatureDiscoverer : TraitDiscovererBase,ITraitDiscoverer
{
    public const string TypeName = $"{TraitDiscovererBase.AssemblyName}.Helpers.CustomTraits.FeatureDiscoverer";

    protected override string CategoryName => "Feature";
    public override IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return GetCategory();
        var id = traitAttribute.GetNamedArgument<string>("Id");
        if (!string.IsNullOrEmpty(id))
        {
            yield return new (TypeName, id);
        }
    }
}
