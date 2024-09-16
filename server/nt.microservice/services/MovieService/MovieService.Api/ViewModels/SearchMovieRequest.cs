namespace MovieService.Api.ViewModels;

public record SearchMovieRequest
{
    public required string SearchTerm { get; set; }
}

public record SearchMovieResponse
{
}
