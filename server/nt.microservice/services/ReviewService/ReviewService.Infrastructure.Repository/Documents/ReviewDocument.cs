using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace ReviewService.Infrastructure.Repository.Documents;

[Collection(ReviewDocument.CollectionName)]
public class ReviewDocument:Entity
{
    internal const string CollectionName = "reviews";

    
    [BsonElement("movieId")]
    public Guid MovieId { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("content")]
    public string Content { get; set; } = string.Empty;

    [BsonElement("rating")]

    public int Rating { get; set; }

    [BsonElement("author")]
    public string Author { get; set; } = string.Empty;

    [BsonElement("createdOn")]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public IEnumerable<string> UpvotedBy { get; set; } = [];

    public IEnumerable<string> DownvotedBy { get; set; } = [];

}
