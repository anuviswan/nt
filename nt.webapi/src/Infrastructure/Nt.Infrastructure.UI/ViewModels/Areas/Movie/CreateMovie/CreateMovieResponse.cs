using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie
{
    public class CreateMovieResponse : IErrorInfo
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ErrorMessage { get; set; }
    }
}
