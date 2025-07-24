using AutoMapper;
using MongoDB.Driver;
using ReviewService.Domain.Entities;
using ReviewService.Domain.Repositories;
using ReviewService.Infrastructure.Repository.Documents;

namespace ReviewService.Infrastructure.Repository.Repositories;

public class ReviewRepository(IMongoDatabase mongoDatabase,IMapper mapper) : GenericRepository<ReviewDocument,Review>(mongoDatabase,mapper, "Reviews"), IReviewRepository
{
    public async Task<IEnumerable<Review>> GetRecentReviewsForUsersAsync(IEnumerable<string> userIds, int count = 10)
    {
        var filter = Builders<ReviewDocument>.Filter.In(r => r.Author, userIds.Select(u => u.ToString()));
        var result = await Collection
            .Find(filter)
            .SortByDescending(r => r.CreatedOn)
            .Limit(count).ToCursorAsync().ConfigureAwait(false);
        return Mapper.Map<IEnumerable<Review>>(result.ToEnumerable()) ?? throw new InvalidOperationException("No recent reviews found for the specified users.");
    }

    public async Task<IEnumerable<Review>> GetReviewsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        
        var filter = Builders<ReviewDocument>.Filter.And(
            Builders<ReviewDocument>.Filter.Gte(r => r.CreatedOn, startDate),
            Builders<ReviewDocument>.Filter.Lte(r => r.CreatedOn, endDate)
        );

        if (startDate == DateTime.MinValue && endDate == DateTime.MaxValue)
        {
            filter = Builders<ReviewDocument>.Filter.Empty; // No date range filter
        }

        var result = await Collection
            .Find(filter)
            .ToCursorAsync()
            .ConfigureAwait(false);

        return Mapper.Map<IEnumerable<Review>>(result.ToEnumerable() ?? throw new InvalidOperationException("No reviews found in the specified date range."));
    }

    public async Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId)
    {
        var filter = Builders<ReviewDocument>.Filter.Eq(r => r.MovieId, movieId);
        var result = await Collection
            .Find(filter)
            .ToCursorAsync().ConfigureAwait(false);

        return Mapper.Map<IEnumerable<Review>>(result.ToEnumerable() ?? throw new InvalidOperationException("No reviews found for the specified movie."));
    }

    public async Task<IEnumerable<Review>> GetReviewsByRatingAsync(int rating)
    {
        var filter = Builders<ReviewDocument>.Filter.Eq(r => r.Rating, rating);
        var result = await Collection
            .Find(filter)
            .ToCursorAsync()
            .ConfigureAwait(false);

        return Mapper.Map<IEnumerable<Review>>(result.ToEnumerable() ?? throw new InvalidOperationException("No reviews found with the specified rating."));
    }

    public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId)
    {
        var filter = Builders<ReviewDocument>.Filter.Eq(r => r.Author, userId.ToString());
        var result = await Collection
            .Find(filter)
            .ToCursorAsync()
            .ConfigureAwait(false);

        return Mapper.Map<IEnumerable<Review>>(result.ToEnumerable() ?? throw new InvalidOperationException("No reviews found for the specified user."));
    }
}
