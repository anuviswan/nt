namespace MovieService.Api.GraphQL.Models;

public record Movie
{
    public string Title { get; set; }
    public string Language { get; set; }
    public DateTime ReleaseDate { get; set; }
}
