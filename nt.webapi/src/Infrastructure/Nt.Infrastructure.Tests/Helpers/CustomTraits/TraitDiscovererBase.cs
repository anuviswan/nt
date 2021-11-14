using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Helpers.CustomTraits;
public class TraitDiscovererBase : ITraitDiscoverer
{
    public const string AssemblyName = "Nt.Infrastructure.Tests";
    protected const string Category = nameof(Category);
    protected virtual string CategoryName => nameof(CategoryName);

    protected KeyValuePair<string,string> GetCategory()
    {
        return new (Category, CategoryName);
    }
    public virtual IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        return Enumerable.Empty<KeyValuePair<string,string>>();
    }
}
