using AutoMapper;
using Nt.WebApi.Models.RequestObjects;
using Nt.WebApi.Models.ResponseObjects;
using Nt.WebApi.Shared.Entities;

namespace Nt.WebApi.Profiles
{
    public class MovieEntityProfile:Profile
    {
        public MovieEntityProfile()
        {
            CreateMap<CreateMovieRequest, MovieEntity>()
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.MovieName))
                    .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director))
                    .ReverseMap(); 
            CreateMap<MovieEntity, MovieResponse>()
                     .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                    .ForMember(dest => dest.Director, opt => opt.MapFrom(src => src.DirectorName)); 

        }
    }
}
