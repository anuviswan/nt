using Nt.Domain.Entities.Attributes;
using Nt.Domain.Entities.Entities;

namespace Nt.Domain.Entities.Movie
{
    [BsonCollection("Review")]
    public record ReviewEntity : BaseEntity
    {
        public string ReviewTitle { get; init; }
        public string ReviewDescription { get; init; }
    }
}
