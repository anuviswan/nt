using AutoMapper;

namespace ReviewService.Presenation.Api.Maps;

public class ProfileMap:Profile
{
    public ProfileMap()
    {
        // Define your mappings here
        // Example: CreateMap<SourceType, DestinationType>();

        CreateMap<Domain.Entities.ReviewEntity,Infrastructure.Repository.Documents.ReviewDocument>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId.ToString()))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
            .ForMember(dest => dest.UpvotedBy, opt => opt.MapFrom(src => src.UpvotedBy.Select(u => u.ToString())))
            .ForMember(dest => dest.DownvotedBy, opt => opt.MapFrom(src => src.DownvotedBy.Select(u => u.ToString())))
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.ID))); 

        CreateMap<Domain.Entities.ReviewEntity, Application.DTO.Reviews.ReviewDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId.ToString()))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
            .ForMember(dest => dest.UpvotedBy, opt => opt.MapFrom(src => src.UpvotedBy.Select(u => u.ToString())))
            .ForMember(dest => dest.DownvotedBy, opt => opt.MapFrom(src => src.DownvotedBy.Select(u => u.ToString())))
            .ReverseMap();

    }
}
