using MongoDB.Bson.Serialization.Attributes;
using Nt.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Models
{
    public class MovieDto : BaseDto
    {
        [BsonElement("movieTitle")]
        public string Title { get; set; }
        [BsonElement("releaseDate")]
        public DateTime ReleaseDate { get; set; }
        [BsonElement("directorName")]
        public string DirectorName { get; set; }
    }
}
