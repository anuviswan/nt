using MongoDB.Driver;
using ReviewService.Domain.Entities;
using ReviewService.Domain.Repositories;

namespace ReviewService.Infrastructure.Repository.Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(IMongoDatabase mongoDatabase):base(mongoDatabase, "Reviews")
    {
            
    }
    public async Task<IEnumerable<Review>> GetRecentReviewsForUsersAsync(IEnumerable<Guid> userIds, int count = 10)
    {
        var filter = Builders<Review>.Filter.In(r => r.Author, userIds.Select(u => u.ToString()));
        return (await Collection
            .Find(filter)
            .SortByDescending(r => r.CreatedOn)
            .Limit(count).ToCursorAsync().ConfigureAwait(false)).ToEnumerable();
    }

    public async Task<IEnumerable<Review>> GetReviewsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var filter = Builders<Review>.Filter.And(
            Builders<Review>.Filter.Gte(r => r.CreatedOn, startDate),
            Builders<Review>.Filter.Lte(r => r.CreatedOn, endDate)
        );

        return (await Collection
            .Find(filter)
            .ToCursorAsync()
            .ConfigureAwait(false)).ToEnumerable();
    }

    public async Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.MovieId, movieId.ToString());
        return (await Collection
            .Find(filter)
            .ToCursorAsync().ConfigureAwait(false)).ToEnumerable();
    }

    public async Task<IEnumerable<Review>> GetReviewsByRatingAsync(int rating)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.Rating, rating);
        return (await Collection
            .Find(filter)
            .ToCursorAsync()
            .ConfigureAwait(false)).ToEnumerable();
    }

    public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.Author, userId.ToString());
        return (await Collection
            .Find(filter)
            .ToCursorAsync()
            .ConfigureAwait(false)).ToEnumerable();
    }
}
