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
        public static List<UserProfileEntity> UserCollection { get; set; } = Users.GetUserCollection().ToList();
        public static List<ReviewEntity> ReviewCollection { get; set; } = Reviews.GetReviewCollection().ToList();
        public static List<MovieEntity> MovieCollection { get; set; } = Movies.GetMovieCollection().ToList();




        internal static MovieEntity GetMovie(string movieId)
        {
            var reviews = ReviewCollection.Where(review => review.MovieId == movieId);
            var movieReview = MovieCollection.Where(c => c.Id == movieId)
                                .Select(movie => new MovieEntity
                                {
                                    Id = movie.Id,
                                    Director = movie.Director,
                                    PlotSummary = movie.PlotSummary,
                                    Language = movie.Language,
                                    Title = movie.Title,
                                    ReleaseDate = movie.ReleaseDate,
                                    CastAndCrew = movie.CastAndCrew,
                                    TotalReviews = reviews.Count(),
                                    Rating = reviews.Any() ? reviews.Average(x=>x.Rating):0
                                }).Single();

            return movieReview;
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
