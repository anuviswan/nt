using AutoMapper;
using ReviewService.Application.DTO.Reviews;
using ReviewService.Application.Interfaces.Services;
using ReviewService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Application.Services.Services;

public class ReviewCachingService(ICachingService cachingService, IMapper mapper) : IReviewCachingService
{
    protected ICachingService CachingService => cachingService;
    protected IMapper Mapper => mapper;
    public async Task SaveInCache(ReviewDto review)
    {
        var cacheKey = $"user:{review.UserName}:recentReviews";

        // Cache the review for future requests
        await CachingService.SetAsync(cacheKey, review, TimeSpan.FromMinutes(5)).ConfigureAwait(false);
    }
}
