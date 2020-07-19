using System;

namespace Nt.Infrastructure.WebApi.ViewModels.RequestObjects
{
    public class CreateMovieRequest
    {
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
    }
}
