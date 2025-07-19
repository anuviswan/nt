using MongoDB.Entities;

namespace ReviewService.Infrastructure.Repository.Documents;

[Collection(ReviewDocument.CollectionName)]
public class ReviewDocument:Entity
{
    internal const string CollectionName = "reviews";

    [Field("reviewId")]
    public Guid ReviewId { get; set; } = Guid.NewGuid();

    [Field("movieId")]
    public Guid MovieId { get; set; }

    [Field("title")]
    public string Title { get; set; } = string.Empty;

    [Field("content")]
    public string Content { get; set; } = string.Empty;

    [Field("rating")]
    public int Rating { get; set; }

    [Field("userName")]
    public string UserName { get; set; } = string.Empty;
    
}
