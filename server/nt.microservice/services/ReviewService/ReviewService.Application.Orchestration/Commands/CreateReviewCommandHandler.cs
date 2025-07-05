using MediatR;
using ReviewService.Application.Interfaces.Operations;

namespace ReviewService.Application.Orchestration.Commands;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IReviewService _reviewService;
    public CreateReviewCommandHandler(IReviewService reviewService)
    {
        _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
    }

    public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        return await _reviewService.CreateReviewAsync(request.Review).ConfigureAwait(false);
    }
}
