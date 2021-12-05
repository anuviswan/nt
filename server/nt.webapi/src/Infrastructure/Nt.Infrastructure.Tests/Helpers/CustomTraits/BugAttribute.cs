using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Helpers.CustomTraits;
public class BugAttribute:Attribute,ITraitAttribute
{
    public string Id { get; set; }
    public BugAttribute(string id) => Id = id;
    public BugAttribute() { }
}
