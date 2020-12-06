using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Nt.Domain.Entities.Attributes;
using Nt.Domain.Entities.Entities;

namespace Nt.Domain.Entities.Movie
{
    [BsonCollection("Review")]
    public record ReviewEntity : BaseEntity
    {
        [BsonElement("reviewTitle")]
        public string ReviewTitle { get; init; }
        [BsonElement("reviewDescription")]
        public string ReviewDescription { get; init; }
        [BsonElement("rating")]
        public double Rating { get; init; }
        [BsonElement("authorId")]
        public string AuthorId { get; init; }
        [BsonElement("movieId")]
        public string MovieId { get; init; }
        [BsonElement("upvotedBy")]
        public IEnumerable<string> UpVotedBy { get; init; }
        [BsonElement("downvotedBy")]
        public IEnumerable<string> DownVotedBy { get; init; }
    }
}
