namespace MovieService.Api.ViewModels;

public record CreateMovieRequest
{
    public required string Title { get; init; }
    public required string Language { get; init; }
    public required DateOnly? ReleaseDate { get; init; }
}


public record CreateMovieResponse
{

}