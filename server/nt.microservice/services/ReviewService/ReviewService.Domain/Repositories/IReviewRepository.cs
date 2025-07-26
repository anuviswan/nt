using ReviewService.Domain.Entities;

namespace ReviewService.Domain.Repositories;

public interface IReviewRepository : IGenericRepository<ReviewEntity>
{
    Task<IEnumerable<ReviewEntity>> GetReviewsByMovieIdAsync(Guid movieId);
    Task<IEnumerable<ReviewEntity>> GetReviewsByUserIdAsync(Guid userId);
    Task<IEnumerable<ReviewEntity>> GetReviewsByRatingAsync(int rating);
    Task<IEnumerable<ReviewEntity>> GetReviewsByDateRangeAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<ReviewEntity>> GetRecentReviewsForUsersAsync(IEnumerable<string> userIds, int count = 3);
}