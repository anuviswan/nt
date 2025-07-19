using MediatR;
using ReviewService.Application.DTO.Reviews;
using ReviewService.Application.Interfaces.Operations;

namespace ReviewService.Application.Orchestration.Queries
{
    public class GetRecentReviewsForUsersQueryHandler : IRequestHandler<GetRecentReviewsForUsersQuery, IEnumerable<ReviewDto>>
    {
        private readonly IReviewService _reviewService;
        public GetRecentReviewsForUsersQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        public async Task<IEnumerable<ReviewDto>> Handle(GetRecentReviewsForUsersQuery request, CancellationToken cancellationToken)
        {
            return await _reviewService.GetRecentReviewsForUsersAsync(request.UserIds, request.Count).ConfigureAwait(false); ;
        }
    }
}
