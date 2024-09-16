using nt.shared.dto.Attributes;

namespace MovieService.Api.ViewModels;

public record CreateMovieRequest
{
    public required string Title { get; init; }
    public required string Language { get; init; }

    [TechnicalDebt(DebtType.MissingFeature, "Missing support for DateOnly in MongoDb, will use DateTime until then")]
    public required DateTime? ReleaseDate { get; init; }
}
