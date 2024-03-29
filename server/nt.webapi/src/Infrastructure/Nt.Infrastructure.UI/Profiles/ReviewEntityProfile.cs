﻿using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.CreateReview;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetAllReviews;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetRecentReviews;

namespace Nt.Infrastructure.WebApi.Profiles;
public class ReviewEntityProfile:BaseProfileMapper
{
    public ReviewEntityProfile():base()
    {
    }

    protected override void FromEntity()
    {
        CreateMap<ReviewEntity, CreateReviewResponse>();
        CreateMap<ReviewDto, ReviewItem>();
        CreateMap<UserDto, AuthorInfo>();
        CreateMap<MovieDto, MovieInfo>();
        CreateMap<MovieReviewDto, GetAllReviewsResponse>();
        CreateMap<MovieReviewDto, GetRecentReviewsResponse>();
        CreateMap<ReviewDto, RecentReviewItem>();
        CreateMap<UserDto, AuthorInfoItem>();
        CreateMap<MovieDto, MovieInfoItem>();
    }

    protected override void ToEntity()
    {
        CreateMap<CreateReviewRequest, ReviewEntity>()
            .ForMember(x => x.ReviewTitle, opt => opt.MapFrom(c => c.Title))
            .ForMember(x => x.ReviewDescription, opt => opt.MapFrom(c=>c.Description));
    }
}
