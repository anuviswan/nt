using MovieService.Data.Interfaces.Entities;

namespace MovieService.Data.Seed;

public class Seed
{
    public static IEnumerable<MovieEntity> Movies => [..MalayalamMovies, ..EnglishMovies, ..TamilMovies];
    private static IEnumerable<MovieEntity> MalayalamMovies => MalayalamMoviesSeed.Movies;    
    private static IEnumerable<MovieEntity> EnglishMovies => EnglishMoviesSeed.Movies;    
    private static IEnumerable<MovieEntity> TamilMovies => [];    
}
