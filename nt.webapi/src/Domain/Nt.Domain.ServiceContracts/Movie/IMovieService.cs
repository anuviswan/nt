using System.Collections.Generic;
using System.Threading.Tasks;
using Nt.Domain.Entities.Movie;

namespace Nt.Domain.ServiceContracts.Movie
{
    public interface IMovieService
    {
        Task<MovieEntity> CreateAsync(MovieEntity movie);
        Task<MovieEntity> GetOne(MovieEntity movie);
        Task<IEnumerable<MovieEntity>> SearchMovie(string partialTitle,int maxCount=-1);
    }
}
