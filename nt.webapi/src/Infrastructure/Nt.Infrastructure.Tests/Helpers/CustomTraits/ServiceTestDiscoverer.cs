using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Helpers.CustomTraits
{
    public class ServiceTestDiscoverer : ITraitDiscoverer
    {
        public const string TypeName = TraitDiscovererBase.AssemblyName + ".Helpers.CustomTraits.ServiceTestDiscoverer";
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            yield return new KeyValuePair<string, string>("Group", "Service");
            var name = traitAttribute.GetNamedArgument<string>("Name");
            if (!string.IsNullOrEmpty(name))
            {
                yield return new KeyValuePair<string, string>("Service", name);
            }
        }
    }
}
