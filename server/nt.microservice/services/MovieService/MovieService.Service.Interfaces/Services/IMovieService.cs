using MovieService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Service.Interfaces.Services;

public interface IMovieService
{
    Task<Movie> CreateMovie(Movie movie);
}
