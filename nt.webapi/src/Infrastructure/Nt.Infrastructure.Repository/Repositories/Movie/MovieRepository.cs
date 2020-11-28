using MongoDB.Driver;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Infrastructure.Data.Repositories.Movie
{
    public class MovieRepository : GenericRepository<MovieEntity>, IMovieRepository
    {
        public MovieRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {

        }
    }
}
