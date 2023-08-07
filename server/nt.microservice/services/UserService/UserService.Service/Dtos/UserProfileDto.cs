namespace UserService.Service.Dtos;

public record UserProfileDto
{
    public string? UserName { get; set; }
    public string? DisplayName { get; set; }

    public List<long> Followers { get; set; }

    public List<long> FollowedBy { get; set; }
    public string? Bio { get; set; }
}
