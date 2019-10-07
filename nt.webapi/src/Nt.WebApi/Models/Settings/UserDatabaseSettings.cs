using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Models.Settings
{
    public class UserDatabaseSettings : IUserDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }


    public interface IUserDatabaseSettings : IDatabaseSettings
    {

    }

}
