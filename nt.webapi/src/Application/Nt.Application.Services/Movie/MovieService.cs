using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JExtensions.Linq;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.Movie;

namespace Nt.Application.Services.Movie
{
    public class MovieService : ServiceBase, IMovieService
    {
        public MovieService(IUnitOfWork unitOfWork):base(unitOfWork)
        {

        }
        public async Task<MovieEntity> CreateAsync(MovieEntity movie)
        {
            var existingMovie = await UnitOfWork.MovieRepository.GetAsync(x => x.Title.ToLower() == movie.Title.ToLower()
            && x.Language.ToLower() == movie.Language.ToLower());

            if (existingMovie.Any(x => x.ReleaseDate.Year == movie.ReleaseDate.Year))
            {
                throw new EntityAlreadyExistException();
            }

            var result = await UnitOfWork.MovieRepository.CreateAsync(movie).ConfigureAwait(false);
            return result;
        }

        public async Task<MovieReviewDto> GetOne(string movieId)
        {
            var movies = await UnitOfWork.MovieRepository.GetAsync(x => x.Id == movieId);

            return movies.Count() switch
            {
                0 => throw new EntityNotFoundException(),
                1 => await _(movies),
                _ => throw new MultipleEntityFoundException()
            };


            async Task<MovieReviewDto> _(IEnumerable<MovieEntity> movieResult)
            {
                var movie = movieResult.Single();
                var movieId = movie.Id;
                var reviews = await UnitOfWork.ReviewRepository.GetAsync(x => x.MovieId == movie.Id);

                var movieDetailed = new MovieReviewDto
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PlotSummary = movie.PlotSummary,
                    Director = movie.Director,
                    Language = movie.Language,
                    ReleaseDate = movie.ReleaseDate,
                    CastAndCrew = movie.CastAndCrew
                };

                var reviewCollection = new List<ReviewDto>();
                foreach (var review in reviews)
                {
                    var user = await UnitOfWork.UserProfileRepository.GetAsync(x => x.Id == review.AuthorId)
                        .ContinueWith((users) => users.Result.Single());

                    reviewCollection.Add(new ReviewDto
                    {
                        Author = new UserDto { Id = user.Id, DisplayName = user.DisplayName, UserName = user.UserName },
                        Description = review.ReviewDescription,
                        Id = review.Id,
                        Title = review.ReviewTitle,
                        DownvotedBy = review.DownVotedBy,
                        UpvotedBy = review.UpVotedBy,
                        Rating = review.Rating
                    });
                }

                return movieDetailed with { Reviews = reviewCollection };
            }
        }

        public async Task<List<MovieEntity>> SearchMovie(string partialTitle,int maxCount = -1)
        {
            if (string.IsNullOrEmpty(partialTitle))
                return Enumerable.Empty<MovieEntity>().ToList();

            var result = await UnitOfWork.MovieRepository.GetAsync(x => x.Title.ToLower().Contains(partialTitle.ToLower()));
            var resultCollection = new List<MovieEntity>();
            foreach (var movieItem in (maxCount==-1?result:result.Take(maxCount)))
            {
                var reviews = await UnitOfWork.ReviewRepository.GetAsync(x => Equals(x.MovieId, movieItem.Id));
                resultCollection.Add(movieItem with
                {
                    TotalReviews = reviews.Count(),
                    Rating = reviews.Any() ? reviews.Average(x => x.Rating) : 0
                });
            }
            return resultCollection;
        }
       
    }
}
