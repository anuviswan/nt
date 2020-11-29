using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nt.Domain.Entities.Movie;
using Nt.Infrastructure.WebApi.ViewModels.Areas.Review;

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
        }

        protected override void ToEntity()
        {
            CreateMap<CreateReviewRequest, ReviewEntity>();
        }
    }
}
