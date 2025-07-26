using ReviewService.Application.DTO.Reviews;

namespace ReviewService.Application.Interfaces.Services;

public interface IReviewCachingService
{
    Task SaveInCache(ReviewDto review);
    Task<ReviewDto?> ReadCache(Guid reviewId);
    Task<IEnumerable<ReviewDto>> ReadUserRecentReviews(string userName, int count = 10);
}
