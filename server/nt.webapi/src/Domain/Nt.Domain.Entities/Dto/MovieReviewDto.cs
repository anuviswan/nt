namespace Nt.Domain.Entities.Dto;
public record MovieReviewDto
{
    public IEnumerable<ReviewDto> Reviews { get; init; } = Enumerable.Empty<ReviewDto>();
}

public record ReviewDto
{
    public string Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public IEnumerable<string> UpvotedBy { get; init; } = Enumerable.Empty<string>();
    public IEnumerable<string> DownvotedBy { get; init; } = Enumerable.Empty<string>();
    public UserDto Author { get; set; }
    public MovieDto Movie { get; init; }
    public double Rating { get; init; }
}

public record MovieDto(string Id,string Title);

public record UserDto
{
    public string Id { get; init; }
    public string UserName { get; init; }
    public string DisplayName { get; init; }
    public int Followers { get; set; }
}
