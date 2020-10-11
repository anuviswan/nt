using System;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie
{
    public class CreateMovieResponse : ResponseBase
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
