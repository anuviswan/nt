using AutoMapper;
using ReviewService.Application.DTO.Reviews;
using ReviewService.Application.Interfaces.Services;
using ReviewService.Application.Services.Extensions;

namespace ReviewService.Application.Services.Services;

public class ReviewCachingService(ICachingService cachingService, IMapper mapper) : IReviewCachingService
{
    protected ICachingService CachingService => cachingService;
    protected IMapper Mapper => mapper;

    public async Task SaveInCache(ReviewDto review)
    {
        var reviewCacheKey = GenerateReviewCacheKey(review.Id);
        var sortedCacheKey = GenerateUserRecentReviewsCacheKey(review.Author);

        // Cache the review for future requests
        await CachingService.StringSetAsync(reviewCacheKey, review).ConfigureAwait(false);
        await CachingService.SortedSetAsync(sortedCacheKey, reviewCacheKey, review.CreatedOn.ToUnixTimestamp()).ConfigureAwait(false);
    }

    public async Task<ReviewDto?> ReadCache(Guid reviewId)
    {
        var reviewCacheKey = GenerateReviewCacheKey(reviewId);
        return await CachingService.StringGetAsync<ReviewDto>(reviewCacheKey).ConfigureAwait(false);
    }

    public async Task<IEnumerable<ReviewDto>> ReadUserRecentReviews(string userName, int count = 10)
    {
        var sortedCacheKey = GenerateUserRecentReviewsCacheKey(userName);
        var reviewKeys = await CachingService.SortedSetRangeByScoreAsync<string>(sortedCacheKey, count).ConfigureAwait(false);
        var reviews = new List<ReviewDto>();
        foreach (var reviewKey in reviewKeys)
        {
            var review = await CachingService.StringGetAsync<ReviewDto>(reviewKey).ConfigureAwait(false);
            if (review != null)
            {
                reviews.Add(review);
            }
        }

        return reviews.OrderByDescending(r => r.CreatedOn).ToList();
    }

    private static string GenerateReviewCacheKey(Guid id) => $"review:{id}";

    private static string GenerateUserRecentReviewsCacheKey(string userName) => $"user:{userName}:recentReviews";
}
