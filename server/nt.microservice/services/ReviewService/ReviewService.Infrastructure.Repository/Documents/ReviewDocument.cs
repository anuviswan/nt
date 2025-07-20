using MongoDB.Entities;

namespace ReviewService.Infrastructure.Repository.Documents;

[Collection(ReviewDocument.CollectionName)]
public class ReviewDocument:Entity
{
    internal const string CollectionName = "reviews";

    
    [Field("movieId")]
    public Guid MovieId { get; set; }

    [Field("title")]
    public string Title { get; set; } = string.Empty;

    [Field("content")]
    public string Content { get; set; } = string.Empty;

    [Field("rating")]
    public int Rating { get; set; }

    [Field("author")]
    public string Author { get; set; } = string.Empty;

    [Field("createdOn")]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

}
