namespace ReviewService.Application.DTO.Reviews;

public class  ReviewDto
{
    public Guid Id { get; set; }
    public Guid MovieId { get; set; }

    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Author { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

}
