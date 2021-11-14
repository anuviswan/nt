using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Helpers.CustomTraits;
public class BugDiscoverer : TraitDiscovererBase, ITraitDiscoverer
{
    public const string TypeName = TraitDiscovererBase.AssemblyName + ".Helpers.CustomTraits.BugDiscoverer";

    protected override string CategoryName => "Bug";
    public override IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return GetCategory();
        var id = traitAttribute.GetNamedArgument<string>("Id");
        if (!string.IsNullOrEmpty(id))
        {
            yield return new (CategoryName, id);
        }
    }
}
