﻿using Nt.Domain.Entities.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.CreateMovie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.GetMovie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Movie.SearchMovieByTitle;

namespace Nt.Infrastructure.WebApi.Profiles;
public class MovieEntityProfile:BaseProfileMapper
{
    public MovieEntityProfile():base()
    {
           
    }

    protected override void FromEntity()
    {
        CreateMap<MovieEntity, CreateMovieResponse>();
        CreateMap<MovieEntity, SearchMovieByTitleResponse>();
        CreateMap<MovieEntity, GetMovieResponse>()
            .ForMember(x => x.Tags, opt => opt.MapFrom(x => x.CastAndCrew))
            .ForMember(x=> x.Rating, opt => opt.MapFrom(x=> x.Rating))
            .ForMember(x=> x.CountReviews, opt=> opt.MapFrom(x=> x.TotalReviews));

    }

    protected override void ToEntity()
    {
        CreateMap<CreateMovieRequest, MovieEntity>();
        CreateMap<CreateMovieResponse,MovieEntity>();
    }
}
