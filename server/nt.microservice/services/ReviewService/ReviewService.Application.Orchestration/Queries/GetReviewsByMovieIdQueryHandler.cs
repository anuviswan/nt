using MediatR;
using ReviewService.Application.DTO.Reviews;
using ReviewService.Application.Interfaces.Operations;

namespace ReviewService.Application.Orchestration.Queries;

public class GetReviewsByMovieIdQueryHandler : IRequestHandler<GetReviewsByMovieIdQuery, IEnumerable<Review>>
{
    private readonly IReviewService _reviewService;
    public GetReviewsByMovieIdQueryHandler(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public Task<IEnumerable<Review>> Handle(GetReviewsByMovieIdQuery request, CancellationToken cancellationToken)
    {
        return _reviewService.GetReviewsByMovieIdAsync(request.MovieId);
    }
}
