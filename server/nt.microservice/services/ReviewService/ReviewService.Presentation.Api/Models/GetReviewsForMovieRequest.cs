namespace ReviewService.Presenation.Api.Models;

public record GetReviewsForMovieRequest
{
    public Guid MovieId { get; set; }
}

public record GetReviewsForMovieResponse
{
    public Guid MovieId { get; set; }
    public IEnumerable<ReviewViewModel> Reviews { get; set; } = [];
}

public record ReviewViewModel
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int UpvotedByCount { get; set; }
    public int DownvotedByCount { get; set; }
}
