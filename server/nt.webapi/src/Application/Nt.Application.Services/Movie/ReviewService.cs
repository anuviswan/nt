﻿using Nt.Domain.Entities.Dto;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.Movie;

namespace Nt.Application.Services.Movie;
public class ReviewService : ServiceBase, IReviewService
{
    public ReviewService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {

    }
    public async Task<ReviewEntity> CreateAsync(ReviewEntity review,string authorUserName)
    {
        var user = await UnitOfWork.UserProfileRepository.GetAsync(x => x.UserName.ToLower() == authorUserName.ToLower() 
                                                                    && x.IsDeleted == false);
        var userID = user.Single().Id;

        var existingReview = await UnitOfWork.ReviewRepository
            .GetAsync(x => x.MovieId == review.MovieId && x.AuthorId == userID)
            .ConfigureAwait(false);

        if (existingReview.Any())
        {
            throw new EntityAlreadyExistException();
        }

        var result = await UnitOfWork.ReviewRepository.CreateAsync(review with { AuthorId = userID }).ConfigureAwait(false);
        return result;
    }

    public async Task<MovieReviewDto> GetAllReviewsAsync(string movieId)
    {
        if (string.IsNullOrEmpty(movieId) || !(await UnitOfWork.MovieRepository.GetAsync(x => movieId == x.Id)).Any())
        {
            throw new ArgumentException("Invalid MovieId");
        }

        var result = new MovieReviewDto();
        var reviews = await UnitOfWork.ReviewRepository.GetAsync(x => x.MovieId == movieId);

        var consolidatedReviews = new List<ReviewDto>();

        foreach(var review in reviews)
        {
            var author = await UnitOfWork.UserProfileRepository.GetAsync(x => x.Id == review.AuthorId);
            consolidatedReviews.Add(new()
            {
                Id = review.Id,
                Description = review.ReviewDescription,
                Rating = review.Rating,
                DownvotedBy = review.DownVotedBy,
                UpvotedBy = review.UpVotedBy,
                Title = review.ReviewTitle,
                Author = author.Select(x => new UserDto
                {
                    DisplayName = x.DisplayName,
                    UserName = x.UserName,
                    Id = x.Id
                }).Single(),
                Movie = new (movieId,string.Empty)
            }); 
        }

        result = result with { Reviews = consolidatedReviews };

        return result;
    }

    public async Task<MovieReviewDto> GetRecentReviewsFromFollowedAsync(string currentUserName,int maxNumberOfItems)
    {
        if (string.IsNullOrEmpty(currentUserName))
        {
            throw new ArgumentException("Invalid UserName");
        }

        var currentUser = await UnitOfWork.UserProfileRepository.GetAsync(x => x.UserName.ToLower() == currentUserName.ToLower());
        var followUsers = currentUser.Single().Follows ?? Enumerable.Empty<string>();

        var reviews = await UnitOfWork.ReviewRepository.FilterReviews(followUsers);
        var result = new MovieReviewDto();
        var reviewResultDto = new List<ReviewDto>();

        foreach (var review in reviews.OrderByDescending(x=>x.CreatedOn).Take(maxNumberOfItems))
        {
            var author = (await UnitOfWork.UserProfileRepository.GetAsync(x => x.Id == review.AuthorId)).Single();
            var movie = (await UnitOfWork.MovieRepository.GetAsync(x => x.Id == review.MovieId)).Single();
            reviewResultDto.Add(new ReviewDto
            {
                Author = new()
                {
                    DisplayName = author.DisplayName,
                    UserName = author.UserName,
                    Id = author.Id,
                    Followers = author.Followers.Count()
                },
                Movie = new (movie.Id,movie.Title),
                Description = review.ReviewDescription,
                Title = review.ReviewTitle,
                Id = review.Id,
                Rating = review.Rating
            });
        }
        return new()
        {
            Reviews = reviewResultDto
        };
    }
}
