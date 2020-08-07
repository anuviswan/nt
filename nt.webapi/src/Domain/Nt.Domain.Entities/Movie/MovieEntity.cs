using MongoDB.Bson.Serialization.Attributes;
using Nt.Domain.Entities.Attributes;
using Nt.Domain.Entities.Entities;
using System;
using System.Collections.Generic;

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
        public string Director { get; set; }
        [BsonElement("language")]
        public string Language { get; set; }
        [BsonElement("actors")]
        public List<string> Actors { get; set; }
        [BsonElement("Reviews")]
        public List<ReviewEntity> Reviews { get; set; }
    }
}
