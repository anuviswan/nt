using MongoDB.Entities;
using nt.shared.dto.Attributes;

namespace MovieService.Data.Interfaces.Entities;

[Collection(MovieEntity.CollectionName)]
public class MovieEntity : Entity
{
    internal const string CollectionName = "movies";

    [Field("title")]
    public string Title { get; set; } = null!;

    [Field("language")]
    public string? Language { get; set; }

    [TechnicalDebt(DebtType.MissingFeature, "Missing support for DateOnly in MongoDb, will use DateTime until then")]
    [Field("releaseDate")]
    public DateTime? ReleaseDate { get; set; }

    [Field("director")]
    public PersonEntity? Director { get; set; }
}


public record PersonEntity
{
    [Field("name")]
    public string Name { get; set; } = null!;
}
