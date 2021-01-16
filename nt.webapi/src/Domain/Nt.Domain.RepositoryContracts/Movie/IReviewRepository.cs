using System.Collections.Generic;
using Nt.Domain.Entities.Movie;

namespace Nt.Domain.RepositoryContracts.Movie
{
    public interface IReviewRepository:IGenericRepository<ReviewEntity>
    {
        public IEnumerable<ReviewEntity> FilterReviews(IEnumerable<string> userIdCollection);
    }
}
