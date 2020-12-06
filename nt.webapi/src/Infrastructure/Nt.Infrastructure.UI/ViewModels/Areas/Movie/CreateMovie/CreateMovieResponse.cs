using System;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie
{
    public record CreateMovieResponse
    {
        public string Title { get; init; }
        public string Language { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string PlotSummary { get; init; }
    }
}
