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
        [Required]
        [MaxLength(200,ErrorMessage = "Plot summary should be maximum 200 characters")]
        [MinLength(3,ErrorMessage = "Plot summary should be minimum 3 characters")]
        public string PlotSummary { get; init; }
    }
}
