using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Nt.Infrastructure.Tests.Helpers.CustomTraits
{
    [AttributeUsage(AttributeTargets.Method)]
    [TraitDiscoverer(ControllerTestDiscoverer.TypeName, TraitDiscovererBase.AssemblyName)]
    public class ControllerTestAttribute:Attribute,ITraitAttribute
    {
        public string Name { get; set; }
        public ControllerTestAttribute(string name) => Name = name;
        public ControllerTestAttribute() { }
    }
}
