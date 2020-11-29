using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Domain.Entities.Movie;

namespace Nt.Domain.ServiceContracts.Movie
{
    public interface IReviewService
    {
        Task<ReviewEntity> CreateAsync(ReviewEntity review);
    }
}
