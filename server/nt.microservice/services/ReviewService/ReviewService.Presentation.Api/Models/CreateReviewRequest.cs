namespace ReviewService.Presenation.Api.Models;

public record CreateReviewRequest
{
    public Guid MovieId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string UserName { get; set; } = string.Empty;
}

public record GetRecentReviewsForUsersRequest(IEnumerable<Guid> UserIds, int Count = 3);

public record GetRecentReviewsForUsersResponse
{
    public IEnumerable<GetRcentReviewsForUserReviewItem> Reviews { get; init; } = [];
};


public record GetRcentReviewsForUserReviewItem
{
    public Guid ReviewId { get; init; }
    public Guid MovieId { get; init; }
    public string MovieTitle { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public int Rating { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string UserDisplayName { get; set; } = string.Empty;
}
