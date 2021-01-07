using System;
using System.Collections.Generic;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.GetMovie
{
    public record GetMovieResponse
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string PlotSummary { get; init; }
        public string Language { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string Genre { get; init; }
        public IEnumerable<string> Tags { get; init; }
        public double Rating { get; init; }
        public long CountReviews { get; init; }
        public string Director { get; init; }
    }

}
