using Nt.WebApi.Shared.Entities;
using Nt.WebApi.Shared.IRepositories;
using Nt.WebApi.Shared.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.WebApi.Repository.Repositories
{
    public class MovieRepository : GenericRepositoryService<MovieEntity, IMovieDatabaseSettings>, IMovieRepository
    {
        public MovieRepository(IMovieDatabaseSettings settings) : base(settings)
        {
        }
    }
}
