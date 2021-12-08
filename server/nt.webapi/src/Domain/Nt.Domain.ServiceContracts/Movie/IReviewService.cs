using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;

namespace Nt.Domain.ServiceContracts.Movie;
public interface IReviewService
{
    Task<ReviewEntity> CreateAsync(ReviewEntity review,string authorUserName);
    Task<MovieReviewDto> GetAllReviewsAsync(string movieId);
    Task<MovieReviewDto> GetRecentReviewsFromFollowedAsync(string currentUserName,int maxReviews);
}
