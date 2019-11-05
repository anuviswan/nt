using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.WebApi.Shared.IRepositories
{
    public interface IReviewRepository : IGenericRepository<ReviewEntity, IReviewDatabaseSettings>
    {
       
    }
}
