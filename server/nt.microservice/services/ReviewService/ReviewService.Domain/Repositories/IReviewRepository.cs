using ReviewService.Domain.Entities;

namespace ReviewService.Domain.Repositories;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId);
    Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId);
    Task<IEnumerable<Review>> GetReviewsByRatingAsync(int rating);
    Task<IEnumerable<Review>> GetReviewsByDateRangeAsync(DateTime startDate, DateTime endDate);
}