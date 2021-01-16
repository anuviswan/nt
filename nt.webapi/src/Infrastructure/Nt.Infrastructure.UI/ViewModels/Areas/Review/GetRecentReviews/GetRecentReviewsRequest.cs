namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetRecentReviews
{
    public record GetRecentReviewsRequest
    {
        public int NumberOfItems { get; init; }
    }
}
