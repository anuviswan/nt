using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nt.Utils.Models;

namespace Nt.Utils.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieReview>> GetRecentReviews();
    }
}
