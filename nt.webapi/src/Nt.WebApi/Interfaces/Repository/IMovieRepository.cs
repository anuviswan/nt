using Nt.WebApi.Models;
using Nt.WebApi.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Interfaces.Repository
{
    public interface IMovieRepository : IGenericRepository<MovieEntity, IMovieDatabaseSettings>
    {
    }
}
