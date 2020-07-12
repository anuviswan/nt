using Nt.WebApi.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.WebApi.Models.ResponseObjects
{
    public class MovieResponse:IErrorInfo
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ErrorMessage { get; set; }
    }
}
