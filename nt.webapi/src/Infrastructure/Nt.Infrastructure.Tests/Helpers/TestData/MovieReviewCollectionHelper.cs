using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;
using Nt.Domain.Entities.User;

namespace Nt.Infrastructure.Tests.Helpers.TestData
{
    public static class MovieReviewCollectionHelper
    {
        public static List<UserProfileEntity> UserCollection { get; set; } = GetUserCollection();
        public static List<ReviewEntity> ReviewCollection { get; set; } = GetReviewCollection();
        public static List<MovieEntity> MovieCollection { get; set; } = GetMovieCollection();


        private static List<UserProfileEntity> GetUserCollection() => Enumerable.Range(1, 10).Select(x => new UserProfileEntity
        {
            Id = string.Format(Utils.MockUserIdFormat, x),
            UserName = $"{nameof(UserProfileEntity.UserName)} {x}",
            DisplayName = $"{nameof(UserProfileEntity.DisplayName)} {x}",
            Bio = $"{nameof(UserProfileEntity.Bio)} {x}",
            IsDeleted = false,
        }).ToList();

        private static List<MovieEntity> GetMovieCollection() => Enumerable.Range(1, 10).Select(x => new MovieEntity
        {
            Id = string.Format(Utils.MockMovieIdFormat, x),
            Title = $"{nameof(MovieEntity.Title)} {x}",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = x % 2 == 0 ? "Drama" : "Thriller",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        }).Concat(new MovieEntity[]
        {
                new MovieEntity
                {
                Id = string.Format(Utils.MockMovieIdFormat,11),
                Title = $"SomeMovie {1}",
                PlotSummary = Utils.TwoHundredCharacterString,
                Language = "English",
                Genre ="Thriller",
                Director = "Will Brown",
                CastAndCrew = new[] { "John Doe","Jane Doe","Jaden Doe" },
                ReleaseDate = Utils.Date,
                Rating = 3,
                TotalReviews = 27,
                }
        }).ToList();


        private static List<ReviewEntity> GetReviewCollection() => new List<ReviewEntity>
        {
            new ReviewEntity
            {
                Id = "R1",
                MovieId = string.Format(Utils.MockMovieIdFormat,1),
                AuthorId= string.Format(Utils.MockUserIdFormat,1),
                ReviewTitle = $"{nameof(ReviewEntity.ReviewTitle)} 01",
                ReviewDescription = $"{nameof(ReviewEntity.ReviewDescription)} 01",
                DownVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 2),
                    string.Format(Utils.MockUserIdFormat, 3),
                    string.Format(Utils.MockUserIdFormat, 4)
                },
                UpVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 5),
                    string.Format(Utils.MockUserIdFormat, 6)
                },
                Rating = 4
            },
            new ReviewEntity
            {
                Id = "R2",
                MovieId = string.Format(Utils.MockMovieIdFormat,1),
                AuthorId= string.Format(Utils.MockUserIdFormat,7),
                ReviewTitle = $"{nameof(ReviewEntity.ReviewTitle)} 02",
                ReviewDescription = $"{nameof(ReviewEntity.ReviewDescription)} 02",
                DownVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 2),
                    string.Format(Utils.MockUserIdFormat, 3),
                    string.Format(Utils.MockUserIdFormat, 4)
                },
                UpVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 1),
                    string.Format(Utils.MockUserIdFormat, 5),
                    string.Format(Utils.MockUserIdFormat, 6)
                },
                Rating = 4
            },
            new ReviewEntity
            {
                Id = "R3",
                MovieId = string.Format(Utils.MockMovieIdFormat,2),
                AuthorId= string.Format(Utils.MockUserIdFormat,7),
                ReviewTitle = $"{nameof(ReviewEntity.ReviewTitle)} 01",
                ReviewDescription = $"{nameof(ReviewEntity.ReviewDescription)} 01",
                DownVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 2),
                    string.Format(Utils.MockUserIdFormat, 3),
                    string.Format(Utils.MockUserIdFormat, 4)
                },
                UpVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 1),
                    string.Format(Utils.MockUserIdFormat, 5),
                    string.Format(Utils.MockUserIdFormat, 6)
                },
                Rating = 3
            }
        };

        internal static MovieReviewDto GetMovieReview(string movieId)
        {
            var movieReview = MovieCollection.Where(c => c.Id == movieId)
                                .Select(movie => new MovieReviewDto
                                {
                                    Id = movie.Id,
                                    Director = movie.Director,
                                    PlotSummary = movie.PlotSummary,
                                    Language = movie.Language,
                                    Title = movie.Title,
                                    ReleaseDate = movie.ReleaseDate,
                                    CastAndCrew = movie.CastAndCrew,
                                    Reviews = ReviewCollection.Where(review => review.MovieId == movieId)
                                                               .Select(review => new ReviewDto
                                                               {
                                                                   Id = review.Id,
                                                                   Description = review.ReviewDescription,
                                                                   Title = review.ReviewTitle,
                                                                   DownvotedBy = review.DownVotedBy,
                                                                   UpvotedBy = review.UpVotedBy,
                                                                   Rating = review.Rating,
                                                                   Author = UserCollection.Where(user => user.Id == review.AuthorId)
                                                                                           .Select(user => new UserDto
                                                                                           {
                                                                                               Id = user.Id,
                                                                                               UserName = user.UserName,
                                                                                               DisplayName = user.DisplayName
                                                                                           }).Single()
                                                               }).ToList()
                                }).Single();

            return movieReview with
            {
                TotalReviews = movieReview.Reviews?.Count()??0,
                Rating = movieReview.Reviews.Any() ? movieReview.Reviews.Average(x => x.Rating):0
            };
        }

        internal static IEnumerable<MovieEntity> GetMovies(string movieTitle)
        {
            return MovieCollection.Where(c => c.Title.Contains(movieTitle,StringComparison.OrdinalIgnoreCase))
                                .Select(movie => new MovieEntity
                                {
                                    Id = movie.Id,
                                    Director = movie.Director,
                                    PlotSummary = movie.PlotSummary,
                                    Language = movie.Language,
                                    Title = movie.Title,
                                    Genre = movie.Genre,
                                    ReleaseDate = movie.ReleaseDate,
                                    CastAndCrew = movie.CastAndCrew,
                                    Rating = ReviewCollection.Where(review => review.MovieId == movie.Id).Any()?ReviewCollection.Where(review => review.MovieId == movie.Id).Average(review => review.Rating):0,
                                    TotalReviews = ReviewCollection.Where(review => review.MovieId == movie.Id).Count(),
                                });

        }
    }
}
