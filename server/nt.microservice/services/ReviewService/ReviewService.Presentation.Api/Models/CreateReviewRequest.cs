namespace ReviewService.Presenation.Api.Models;

public class CreateReviewRequest
{
    public Guid MovieId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string UserName { get; set; } = string.Empty;
}