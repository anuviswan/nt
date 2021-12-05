using Nt.Domain.Entities.Movie;

namespace Nt.Infrastructure.Tests.Helpers.TestData;
public static class Movies
{
    public static IEnumerable<MovieEntity> GetMovieCollection()
    {
        // Drama
        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 1),
            Title = $"{nameof(MovieEntity.Title)} 1",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Drama",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 2),
            Title = $"{nameof(MovieEntity.Title)} 2",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Drama",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 3),
            Title = $"{nameof(MovieEntity.Title)} 3",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Drama",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 4),
            Title = $"{nameof(MovieEntity.Title)} 4",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Drama",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };

        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 5),
            Title = $"{nameof(MovieEntity.Title)} 5",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Drama",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };

        // Thrillers
        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 6),
            Title = $"{nameof(MovieEntity.Title)} 6",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Drama",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 7),
            Title = $"{nameof(MovieEntity.Title)} 7",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Drama",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 8),
            Title = $"{nameof(MovieEntity.Title)} 8",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Drama",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };


        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 9),
            Title = $"{nameof(MovieEntity.Title)} 9",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Drama",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };

        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 10),
            Title = $"{nameof(MovieEntity.Title)} 10",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Drama",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };

        yield return new ()
        {
            Id = string.Format(Utils.MockMovieIdFormat, 11),
            Title = $"SomeMovie 11",
            PlotSummary = Utils.TwoHundredCharacterString,
            Language = "English",
            Genre = "Thriller",
            Director = "Will Brown",
            CastAndCrew = new[] { "John Doe", "Jane Doe", "Jaden Doe" },
            ReleaseDate = Utils.Date,
            Rating = 3,
            TotalReviews = 27,
        };
    }
}
