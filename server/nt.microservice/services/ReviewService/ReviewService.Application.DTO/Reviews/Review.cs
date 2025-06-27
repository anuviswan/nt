namespace ReviewService.Application.DTO.Reviews;

public class  Review
{
    public Guid ReviewId { get; set; }
    public string MovieTitle { get; set; } = null!;
}
