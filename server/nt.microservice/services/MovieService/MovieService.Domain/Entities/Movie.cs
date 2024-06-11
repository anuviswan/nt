namespace MovieService.Domain.Entities;

public class Movie
{
    public string? MovieTitle { get; set; }
    public string? Language { get; set; }
    public DateOnly? ReleaseDate { get; set; }
}
