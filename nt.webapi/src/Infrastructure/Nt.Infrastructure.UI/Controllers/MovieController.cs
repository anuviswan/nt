using AutoMapper;
using Nt.Domain.ServiceContracts.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie;
using System;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.Controllers
{
    public class MovieController:BaseController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMapper mapper, IMovieService movieService) : base(mapper) => _movieService = movieService;

        public Task<CreateMovieResponse>  CreateMovie(CreateMovieRequest movie)
        {
            throw new NotImplementedException();
        }
    }
}
