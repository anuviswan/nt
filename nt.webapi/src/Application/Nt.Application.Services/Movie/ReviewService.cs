﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.Movie;

namespace Nt.Application.Services.Movie
{
    public class ReviewService : ServiceBase, IReviewService
    {
        public ReviewService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<ReviewEntity> CreateAsync(ReviewEntity review,string authorUserName)
        {
            var user = await UnitOfWork.UserProfileRepository.GetAsync(x => x.UserName.ToLower() == authorUserName.ToLower() && x.IsDeleted == false);
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
                consolidatedReviews.Add(new ReviewDto
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
                    Movie = new MovieDto(movieId,string.Empty)
                }); 
            }

            result = result with { Reviews = consolidatedReviews };

            return result;
        }

        public Task<MovieReviewDto> GetRecentReviewsFromFollowed(string currentUserName)
        {
            throw new NotImplementedException();
        }
    }
}
