using ReviewService.Application.DTO.Reviews;

namespace ReviewService.Application.Interfaces.Operations;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetRecentReviewsForUsersAsync(IEnumerable<string> userIds, int count = 3);
    Task<IEnumerable<ReviewDto>> GetReviewsByMovieIdAsync(Guid movieId);
    Task<Guid> CreateReviewAsync(ReviewDto reviewDto);
}
