using System.Collections.Generic;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetAllReviews
{
    public class GetAllReviewsResponse
    {
        public string MovieId { get; set; }
        public IEnumerable<ReviewItem> Reviews { get; set; }
    }

    public class ReviewItem
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public string Rating { get; set; }

        public string Description { get; set; }
        public string AuthorDisplayName { get; set; }
        public string AuthorUserName { get; set; }
        public string AuthorId { get; set; }
    }
}
