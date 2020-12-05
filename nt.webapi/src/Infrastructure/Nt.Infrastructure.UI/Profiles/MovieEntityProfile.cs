using AutoMapper;
using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.GetMovie;
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
            CreateMap<MovieDetailedDto, GetMovieResponse>()
                .ForMember(x => x.Tags, opt => opt.MapFrom(x => x.Actors))
                .ForMember(x=> x.Reviews, opt => opt.Ignore());
        }

        protected override void ToEntity()
        {
            CreateMap<CreateMovieRequest, MovieEntity>();
        }
    }
}
