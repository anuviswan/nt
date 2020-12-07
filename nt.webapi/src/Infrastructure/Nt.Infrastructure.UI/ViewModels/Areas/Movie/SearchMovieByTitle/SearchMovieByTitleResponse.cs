using System;
using System.Collections.Generic;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.SearchMovieByTitle
{
    public record SearchMovieByTitleResponse
    {
        public string Id { get; set; }
        public string Title { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string Language { get; init; }
        public string PlotSummary { get; init; }
        public string Genre { get; init; }
        public double Rating { get; init; }
        public int TotalReviews { get; init; }
        public double NumberOfReviews { get;init; }
        public string Director { get; init; }
        public IEnumerable<string> CastAndCrew { get; init; }
    }
}
