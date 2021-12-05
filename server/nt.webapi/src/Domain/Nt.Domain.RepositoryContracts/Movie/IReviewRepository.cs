using Nt.Domain.Entities.Movie;

namespace Nt.Domain.RepositoryContracts.Movie;
public interface IReviewRepository:IGenericRepository<ReviewEntity>
{
    public Task<IEnumerable<ReviewEntity>> FilterReviews(IEnumerable<string> userIdCollection);
}
