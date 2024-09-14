namespace MovieService.Service.Interfaces.Dtos;

public class MovieDto
{
    public string? Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Language { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Director { get; set; }
}
