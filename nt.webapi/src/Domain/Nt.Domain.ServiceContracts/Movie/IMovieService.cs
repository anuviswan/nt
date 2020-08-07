using Nt.Domain.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Domain.ServiceContracts.Movie
{
    public interface IMovieService
    {
        Task<MovieEntity> CreateAsync(MovieEntity movie);
        Task<MovieEntity> GetOne(MovieEntity movie);

    }
}
