using System;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.SearchMovieByTitle
{
    public record SearchMovieByTitleResponse
    {
        public string Id { get; set; }
        public string Title { get; init; }
        public DateTime ReleaseDate { get; init; }
        public string Language { get; init; }
        // TODO: This would be further developed to link reviews
    }
}
