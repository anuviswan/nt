using ReviewService.Application.DTO.Reviews;

namespace ReviewService.Application.Services.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId);
}
