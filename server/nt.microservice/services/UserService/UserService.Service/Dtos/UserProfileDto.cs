namespace UserService.Service.Dtos;

public record UserProfileDto
{
    public required string UserName { get; init; } 
    public string? DisplayName { get; set; }

    public List<long> Followers { get; set; } = [];

    public List<long> FollowedBy { get; set; } = [];
    public string? Bio { get; set; }
}
