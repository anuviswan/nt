using System.Collections.Generic;
using Nt.Data.Dto.BaseDto;

namespace Nt.Data.Dto.GetRecentReviews;

public class RecentReviewsResponse:BaseResponse
{
    public IEnumerable<RecentReviewItem> Reviews { get; set; }
}


public class RecentReviewItem
{
    public string Id { get; set; }
    public MovieInfoItem Movie { get; set; }
    public string Title { get; set; }
    public double Rating { get; set; }
    public string Description { get; set; }
    public AuthorInfoItem Author { get; set; }
}
public class AuthorInfoItem
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string DisplayName { get; set; }
    public int Followers { get; set; }
}
public class MovieInfoItem
{
    public string Id { get; set; }
    public string Title { get; set; }
} 
