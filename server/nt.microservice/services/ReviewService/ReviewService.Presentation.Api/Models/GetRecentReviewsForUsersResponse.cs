namespace ReviewService.Presenation.Api.Models;

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
    public IEnumerable<string> UpvotedBy { get; set; } = [];
    public IEnumerable<string> DownvotedBy { get; set; } = [];
}
