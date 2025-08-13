namespace MovieService.Api.ViewModels;

public record MovieSearchResult
{
    public required string Title { get;set; }
    public string? MovieLanguage { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Director { get; set; }

}

