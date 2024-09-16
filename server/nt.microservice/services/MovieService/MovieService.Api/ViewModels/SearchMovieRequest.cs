namespace MovieService.Api.ViewModels;

public record SearchMovieRequest
{
    public required string SearchTerm { get; set; }
}

public record MovieSearchResult
{
    public required string Title { get;set; }
    public string? Language { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Director { get; set; }

}
