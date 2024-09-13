using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using nt.shared.dto.Attributes;

namespace MovieService.Domain.Entities;

public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = null!;

    [BsonElement("language")]
    public string? Language { get; set; }

    [TechnicalDebt(DebtType.MissingFeature, "Missing support for DateOnly in MongoDb, will use DateTime until then")]
    [BsonElement("releaseDate")]
    public DateTime? ReleaseDate { get; set; }

    [BsonElement("director")]
    public string? Director { get; set; }
}
