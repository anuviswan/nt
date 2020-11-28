using AutoMapper;
using Nt.Domain.Entities.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.SearchMovieByTitle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nt.Infrastructure.WebApi.Profiles
{
    public class MovieEntityProfile:Profile
    {
        public MovieEntityProfile()
        {
            ToMovieEntity();
            FromMovieEntity();
        }

        private void FromMovieEntity()
        {
            CreateMap<MovieEntity, CreateMovieResponse>();
            CreateMap<MovieEntity, SearchMovieByTitleResponse>();
        }

        private void ToMovieEntity()
        {
            CreateMap<CreateMovieRequest, MovieEntity>();
        }
    }
}
