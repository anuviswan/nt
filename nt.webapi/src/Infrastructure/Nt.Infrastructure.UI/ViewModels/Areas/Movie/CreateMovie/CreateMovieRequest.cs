using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Nt.Infrastructure.WebApi.Attributes;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie
{
    public record CreateMovieRequest
    {
        [Required]
        public string Title { get; init; }
        [Required]
        public string Language { get; init; }
        [Required]
        [DateValidationAttribute(ErrorMessage = "Invalid Date")]
        public DateTime ReleaseDate{get;init;}
        [Required]
        [MaxLength(200,ErrorMessage = "Plot summary should be maximum 200 characters")]
        [MinLength(3,ErrorMessage = "Plot summary should be minimum 3 characters")]
        public string PlotSummary { get; init; }
        public string Genre { get; init; }
        public string Director { get; init; }
        public IEnumerable<string> CastAndCrew { get; init; }
    }
}
