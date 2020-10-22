using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie
{
    public record CreateMovieRequest
    {
        [Required]
        public string Title { get; init; }
        [Required]
        public string Language { get; init; }
        [Required]
        public DateTime ReleaseDate{get;init;}
        public List<string> Actors { get; init; }
    }
}
