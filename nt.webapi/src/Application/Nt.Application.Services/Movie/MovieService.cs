using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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

        public Task<MovieEntity> GetOne(MovieEntity movie)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MovieEntity>> SearchMovie(string partialTitle,int maxCount = -1)
        {
            if(string.IsNullOrEmpty(partialTitle))
            {
                return await Task.FromResult(Enumerable.Empty<MovieEntity>());
            }

            return await UnitOfWork.MovieRepository.GetAsync(x => x.Title.ToLower().Contains(partialTitle.ToLower()));
        }
       
    }
}
