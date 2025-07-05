using MediatR;
using ReviewService.Application.DTO.Reviews;

namespace ReviewService.Application.Orchestration.Queries;

public class GetReviewsByMovieIdQuery : IRequest<IEnumerable<ReviewDto>>
{
    public Guid MovieId { get; set; }
    public GetReviewsByMovieIdQuery(Guid movieId)
    {
        MovieId = movieId;
    }
}
