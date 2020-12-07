using System;
using System.Collections.Generic;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie
{
    public record CreateMovieResponse
    {
        public string Id { get; init; }
        public string Title { get; init; }
        public string Language { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string PlotSummary { get; init; }
        public string Genre { get; init; }
        public IEnumerable<string> CastAndCrew { get; init; }
        public string Director { get; init; }
    }
}
