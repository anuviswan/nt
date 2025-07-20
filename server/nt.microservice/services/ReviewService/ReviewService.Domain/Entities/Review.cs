namespace ReviewService.Domain.Entities;
public class Review : IEntity
{
    public Guid Id { get; set; }
    public string MovieId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Author { get; set; } = null!;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public IEnumerable<string> UpvotedBy { get; set; } = [];
    public IEnumerable<string> DownvotedBy { get; set; } = [];
    public int Rating { get; set; } = 0;
}
