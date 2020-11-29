using System.Collections.Generic;
using Nt.Domain.Entities.Attributes;
using Nt.Domain.Entities.Entities;

namespace Nt.Domain.Entities.Movie
{
    [BsonCollection("Review")]
    public record ReviewEntity : BaseEntity
    {
        public string ReviewTitle { get; init; }
        public string ReviewDescription { get; init; }
        public string AuthorId { get; init; }
        public string MovieId { get; init; }
        public IEnumerable<string> UpVotedBy { get; init; }
        public IEnumerable<string> DownVotedBy { get; init; }
    }
}
