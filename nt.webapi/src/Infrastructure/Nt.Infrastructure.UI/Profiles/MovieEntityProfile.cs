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
    public class MovieEntityProfile:BaseProfileMapper
    {
        public MovieEntityProfile():base()
        {
           
        }

        protected override void FromEntity()
        {
            CreateMap<MovieEntity, CreateMovieResponse>();
            CreateMap<MovieEntity, SearchMovieByTitleResponse>();
        }

        protected override void ToEntity()
        {
            CreateMap<CreateMovieRequest, MovieEntity>();
        }
    }
}
