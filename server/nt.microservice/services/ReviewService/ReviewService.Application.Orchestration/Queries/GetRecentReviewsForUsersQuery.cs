using MediatR;
using ReviewService.Application.DTO.Reviews;

namespace ReviewService.Application.Orchestration.Queries;

public class GetRecentReviewsForUsersQuery : IRequest<IEnumerable<ReviewDto>>
{
    public IEnumerable<Guid> UserIds { get; set; } = [];
    public int Count { get; set; }
}