using MongoDB.Driver;
using ReviewService.Domain.Entities;
using ReviewService.Domain.Repositories;

namespace ReviewService.Infrastructure.Repository.Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(IMongoDatabase mongoDatabase):base(mongoDatabase, "Reviews")
    {
            
    }
    public Task<IEnumerable<Review>> GetRecentReviewsForUsersAsync(IEnumerable<Guid> userIds, int count = 3)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Review>> GetReviewsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Review>> GetReviewsByRatingAsync(int rating)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
