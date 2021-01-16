using System.Collections.Generic;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetRecentReviews
{
    public record GetRecentReviewsResponse
    {
        public IEnumerable<RecentReviewItem> Reviews { get; init; }
    }

    public record RecentReviewItem
    {
        public string Id { get; init; }

        public MovieInfo Movie { get; init; }
        public string Title { get; init; }
        public double Rating { get; init; }

        public string Description { get; init; }
        
        public AuthorInfo Author { get; init; }
    }

    public record AuthorInfo(string Id,string UserName,string DisplayName,int Followers);
    public record MovieInfo(string Id,string Title);
}
