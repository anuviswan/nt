using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie
{
    public class CreateMovieRequest
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate{get;set;}
        public List<string> Actors { get; set; }
    }
}
