namespace ReviewService.Application.DTO.Reviews;

public class  ReviewDto
{
    public Guid ReviewId { get; set; }
    public Guid MovieId { get; set; }

    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string UserName { get; set; } = string.Empty;

}
