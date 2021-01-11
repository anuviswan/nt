using Nt.Domain.Entities.Dto;
using Nt.Domain.Entities.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.CreateReview;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetAllReviews;

namespace Nt.Infrastructure.WebApi.Profiles
{
    public class ReviewEntityProfile:BaseProfileMapper
    {
        public ReviewEntityProfile():base()
        {
        }

        protected override void FromEntity()
        {
            CreateMap<ReviewEntity, CreateReviewResponse>();
            CreateMap<ReviewDto, ReviewItem>();
            CreateMap<MovieReviewDto, GetAllReviewsResponse>();
        }

        protected override void ToEntity()
        {
            CreateMap<CreateReviewRequest, ReviewEntity>()
                .ForMember(x => x.ReviewTitle, opt => opt.MapFrom(c => c.Title))
                .ForMember(x => x.ReviewDescription, opt => opt.MapFrom(c=>c.Description));
        }
    }
}
