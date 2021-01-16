using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts.Movie;

namespace Nt.Infrastructure.Data.Repositories.Movie
{
    public class ReviewRepository : GenericRepository<ReviewEntity>, IReviewRepository
    {
        public ReviewRepository(IMongoDatabase mongoDatabase):base(mongoDatabase)
        {

        }
        public IEnumerable<ReviewEntity> FilterReviews(IEnumerable<string> userIdCollection)
        {
            throw new NotImplementedException();
        }
    }
}
