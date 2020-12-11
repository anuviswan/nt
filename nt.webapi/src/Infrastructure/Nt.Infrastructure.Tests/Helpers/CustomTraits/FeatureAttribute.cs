using System;
using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Helpers.CustomTraits
{
    [TraitDiscoverer(FeatureDiscoverer.TypeName,TraitDiscovererBase.AssemblyName)]
    [AttributeUsage(AttributeTargets.Method)]
    public class FeatureAttribute:Attribute, ITraitAttribute
    {
        public string Id { get; set; }
        public FeatureAttribute(string id) => Id = id;
        public FeatureAttribute() { }
    }
}
