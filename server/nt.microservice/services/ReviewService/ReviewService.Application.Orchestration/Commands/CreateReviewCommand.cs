using MediatR;
using ReviewService.Application.DTO.Reviews;

namespace ReviewService.Application.Orchestration.Commands;

public  class CreateReviewCommand : IRequest<Guid>
{
    public required ReviewDto Review { get; init; }
}
