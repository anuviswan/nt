using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie
{
    public class CreateMovieRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public DateTime ReleaseDate{get;set;}
        public List<string> Actors { get; set; }
    }
}
