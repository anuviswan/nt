using Caliburn.Micro;
using Nt.Data.Dto.GetRecentReviews;
using Nt.Utils.Helper;
using Nt.Utils.Models;
using Nt.Utils.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Utils.Services;

public class MovieService : IMovieService
{
    public async Task<IEnumerable<MovieReview>> GetRecentReviews()
    {
        var request = new RecentReviewsRequest
        {
            NumberOfItems = 10
        };

        var httpService = IoC.Get<IHttpService>();
        var response = await httpService.PostAsync<RecentReviewsRequest, RecentReviewsResponse>(HttpUtils.GetRecentReviewsUrl, request)
            .ConfigureAwait(false);

        if (response.HasError)
        {
            throw new Exception(string.Join(",", response.Errors));
        }

        return response.Reviews.Select(review => new MovieReview
        {
            Author = new User
            {
                Id = review.Author.Id,
                UserName = review.Author.UserName,
                DisplayName = review.Author.DisplayName
            },
            Movie = new Movie
            {
                Id = review.Movie.Id,
                Title = review.Movie.Title
            },
            Id = review.Id,
            Title = review.Title,
            Description = review.Description
        });
    }
    
}
