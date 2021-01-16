using System.Collections.Generic;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetAllReviews
{
    public record GetAllReviewsResponse
    {
        public IEnumerable<ReviewItem> Reviews { get; set; }
    }

    public record ReviewItem
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public double Rating { get; set; }

        public string Description { get; set; }
        public AuthorInfo Author { get; init; }
        public MovieInfo Movie { get; init; }
    }

    public record MovieInfo(string Id,string Title);

    public record AuthorInfo(string Id, string UserName, string DisplayName, int Followers);
}
