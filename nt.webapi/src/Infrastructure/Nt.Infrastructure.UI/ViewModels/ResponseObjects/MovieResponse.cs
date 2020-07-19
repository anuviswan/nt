using Nt.Infrastructure.WebApi.ViewModels.Common;
using System;

namespace Nt.Infrastructure.WebApi.ViewModels.ResponseObjects
{
    public class MovieResponse : IErrorInfo
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ErrorMessage { get; set; }
    }
}
