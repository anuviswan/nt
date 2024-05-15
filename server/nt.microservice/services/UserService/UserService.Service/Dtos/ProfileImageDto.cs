namespace UserService.Service.Dtos;

public record ProfileImageDto
{
    public string ImageKey { get; set; }
    public string UserName { get; set; }
    public MemoryStream FileData { get; set; }
}
