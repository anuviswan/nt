﻿using Nt.Domain.Entities.Movie;

namespace Nt.Domain.ServiceContracts.Movie;
public interface IMovieService
{
    Task<MovieEntity> CreateAsync(MovieEntity movie);
    Task<MovieEntity> GetOne(string movieId);
    Task<List<MovieEntity>> SearchMovie(string partialTitle, int maxCount = -1);
}
