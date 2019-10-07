using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Nt.WebApi.Models
{
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
