using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Shared.Settings
{
    public class MovieDatabaseSettings : IMovieDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }


    public interface IMovieDatabaseSettings : IDatabaseSettings
    {

    }

}
