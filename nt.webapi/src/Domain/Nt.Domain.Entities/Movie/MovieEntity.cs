using MongoDB.Bson.Serialization.Attributes;
using Nt.Domain.Entities.Attributes;
using Nt.Domain.Entities.Entities;
using System;

namespace Nt.Domain.Entities.Movie
{
    [BsonCollection("Movie")]
    public class MovieEntity : BaseEntity
    {
        [BsonElement("movieTitle")]
        public string Title { get; set; }
        [BsonElement("releaseDate")]
        public DateTime ReleaseDate { get; set; }
        [BsonElement("directorName")]
        public string DirectorName { get; set; }
    }
}
