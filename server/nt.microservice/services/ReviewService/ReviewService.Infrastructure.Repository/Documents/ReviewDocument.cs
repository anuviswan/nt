using MongoDB.Entities;

namespace ReviewService.Infrastructure.Repository.Documents;

public class ReviewDocument:Entity
{
    public Guid ReviewId { get; set; } = Guid.NewGuid();
    public Guid MovieId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string UserName { get; set; } = string.Empty;
    
}
