using ReviewService.Application.DTO.Reviews;
using ReviewService.Application.Services.Interfaces;

namespace ReviewService.Application.Services.Operations;

public class ReviewService : IReviewService
{
    public Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId)
    {
        throw new NotImplementedException();
    }
}
