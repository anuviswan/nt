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
            CreateMap<UserDto, UserItem>();
            CreateMap<ReviewDto, ReviewItem>()
                .ForMember(x => x.UpVotes, opt => opt.MapFrom(x => x.UpvotedBy.Any()? x.UpvotedBy.Count():0))
                .ForMember(x => x.DownVotes, opt => opt.MapFrom(x => x.DownvotedBy.Any()? x.DownvotedBy.Count():0));
            CreateMap<MovieDetailedDto, GetMovieResponse>()
                .ForMember(x => x.Tags, opt => opt.MapFrom(x => x.Actors));

        }

        protected override void ToEntity()
        {
            CreateMap<CreateMovieRequest, MovieEntity>();
        }
    }
}
