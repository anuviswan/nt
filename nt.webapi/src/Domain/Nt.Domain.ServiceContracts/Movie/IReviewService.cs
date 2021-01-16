using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;

namespace Nt.Domain.ServiceContracts.Movie
{
    public interface IReviewService
    {
        Task<ReviewEntity> CreateAsync(ReviewEntity review,string authorUserName);
        Task<MovieReviewDto> GetAllReviewsAsync(string movieId);
        Task<MovieReviewDto> GetRecentReviewsFromFollowed(string currentUserName);
    }
}
