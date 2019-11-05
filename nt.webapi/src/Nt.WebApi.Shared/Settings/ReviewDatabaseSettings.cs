using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.WebApi.Shared.Settings
{

    public class ReviewDatabaseSettings : IReviewDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }


    public interface IReviewDatabaseSettings : IDatabaseSettings
    {

    }
}
