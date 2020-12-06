using System.Collections.Generic;
using System.Threading.Tasks;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;

namespace Nt.Domain.ServiceContracts.Movie
{
    public interface IMovieService
    {
        Task<MovieEntity> CreateAsync(MovieEntity movie);
        Task<MovieReviewDto> GetOne(string movieId);
        Task<IEnumerable<MovieEntity>> SearchMovie(string partialTitle,int maxCount=-1);
    }
}
