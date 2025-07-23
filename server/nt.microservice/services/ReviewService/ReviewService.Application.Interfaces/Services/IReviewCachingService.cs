using ReviewService.Application.DTO.Reviews;

namespace ReviewService.Application.Interfaces.Services;

public interface IReviewCachingService
{
    Task SaveInCache(ReviewDto review);
}
