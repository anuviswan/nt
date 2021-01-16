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

        public MovieInfoItem Movie { get; init; }
        public string Title { get; init; }
        public double Rating { get; init; }

        public string Description { get; init; }
        
        public AuthorInfoItem Author { get; init; }
    }

    public record AuthorInfoItem(string Id,string UserName,string DisplayName,int Followers);
    public record MovieInfoItem(string Id,string Title);
}
