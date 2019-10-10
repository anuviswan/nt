using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Shared.Settings
{
    public interface IDatabaseSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
