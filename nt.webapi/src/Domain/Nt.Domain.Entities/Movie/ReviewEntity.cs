using Nt.Domain.Entities.Attributes;
using Nt.Domain.Entities.Entities;

namespace Nt.Domain.Entities.Movie
{
    [BsonCollection("Review")]
    public class ReviewEntity : BaseEntity
    {
        public int MovieId { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewDescription { get; set; }
    }
}
