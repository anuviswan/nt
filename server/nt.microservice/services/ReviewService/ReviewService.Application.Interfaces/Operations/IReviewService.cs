using ReviewService.Application.DTO.Reviews;

namespace ReviewService.Application.Interfaces.Operations;

public interface IReviewService
{
    public Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId);
}
