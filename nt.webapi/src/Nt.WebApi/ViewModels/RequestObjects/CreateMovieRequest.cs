using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.ViewModels.RequestObjects
{
    public class CreateMovieRequest
    {
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
    }
}
