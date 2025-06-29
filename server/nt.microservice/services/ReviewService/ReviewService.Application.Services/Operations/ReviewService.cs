using ReviewService.Application.DTO.Reviews;
using ReviewService.Application.Interfaces.Operations;

namespace ReviewService.Application.Services.Operations;

public class ReviewService : IReviewService
{
    public Task<IEnumerable<Review>> GetReviewsByMovieIdAsync(Guid movieId)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateReviewAsync(Review reviewDto)
    {
        throw new NotImplementedException();
    }
}
