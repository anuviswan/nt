using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

    [BsonElement("release-date")]
    public DateOnly? ReleaseDate { get; set; }

    [BsonElement("director")]
    public string? Director { get; set; }
}
