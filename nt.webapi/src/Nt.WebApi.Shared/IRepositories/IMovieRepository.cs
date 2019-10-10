using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Shared.IRepositories
{
    public interface IMovieRepository : IGenericRepository<MovieEntity, IMovieDatabaseSettings>
    {
    }
}
