using MongoDB.Entities;
using nt.shared.dto.Attributes;

namespace MovieService.Data.Interfaces.Entities;

[Collection(MovieEntity.CollectionName)]
public class MovieEntity : Entity
{
    internal const string CollectionName = "movies";

    [Field("title")]
    public string Title { get; set; } = null!;

    [Field("movie-language")]
    public string? MovieLanguage { get; set; }

    [TechnicalDebt(DebtType.MissingFeature, "Missing support for DateOnly in MongoDb, will use DateTime until then")]
    [Field("releaseDate")]
    public DateTime? ReleaseDate { get; set; }


    [Field("crew")]
    public Dictionary<string, List<PersonEntity>>? Crew { get; set; }

    [Field("cast")]
    public List<PersonEntity> Cast { get; set; }

}

public record PersonEntity
{
    public PersonEntity() { }
    public PersonEntity(string name) => Name = name;

    [Field("name")]
    public string Name { get; set; } = null!;
}
