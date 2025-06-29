using ReviewService.Application.DTO.Reviews;

namespace ReviewService.Application.Interfaces.Operations;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId);
    Task<Guid> CreateReviewAsync(Review reviewDto);
}
