﻿using MongoDB.Bson.Serialization.Attributes;
using Nt.Domain.Entities.Attributes;
using Nt.Domain.Entities.Entities;
using System;
using System.Collections.Generic;

namespace Nt.Domain.Entities.Movie
{
    [BsonCollection("Movie")]
    public record MovieEntity : BaseEntity
    {
        [BsonElement("movieTitle")]
        public string Title { get; init; }
        [BsonElement("plotSummary")]
        public string PlotSummary { get; init; }
        [BsonElement("language")]
        public string Language { get; init; }
        [BsonElement("releaseDate")]
        public DateTime ReleaseDate { get; init; }
        [BsonElement("genre")]
        public string Genre { get; init; }
        public double Rating { get; init; }
        [BsonElement("directorName")]
        public string Director { get; init; }
        [BsonElement("actors")]
        public List<string> Actors { get; init; }
    }
}
