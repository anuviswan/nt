using AutoMapper;
using Microsoft.Extensions.Logging;
using ReviewService.Application.DTO.Reviews;
using ReviewService.Application.Interfaces.Operations;
using ReviewService.Application.Interfaces.Services;
using ReviewService.Domain.Entities;
using ReviewService.Domain.Repositories;

namespace ReviewService.Application.Services.Operations;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly ICachingService _cachingService;
    public ReviewService(IReviewRepository reviewRepository,ICachingService cachingService, IMapper mapper, ILogger<ReviewService> logger)
    {
        _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _cachingService = cachingService ?? throw new ArgumentNullException(nameof(cachingService));
    }
    public Task<IEnumerable<ReviewDto>> GetReviewsByMovieIdAsync(Guid movieId)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> CreateReviewAsync(ReviewDto reviewDto)
    {
        try
        {
            var review = await _reviewRepository.AddAsync(_mapper.Map<ReviewDto, Review>(reviewDto)).ConfigureAwait(false);
            return review.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a review.");
            throw;
        }
    }

    public async Task<IEnumerable<ReviewDto>> GetRecentReviewsForUsersAsync(IEnumerable<Guid> userIds, int count = 3)
    {
        try
        {
            var results = new List<ReviewDto>();
            var nonCachedUsers = new List<Guid>();

            foreach(var id in userIds)
            {
                var cacheKey = $"user:{id}:recentReviews";
                var cachedReviews = await _cachingService.GetAsync<IEnumerable<ReviewDto>>(cacheKey).ConfigureAwait(false);

                if (cachedReviews != null && cachedReviews.Any())
                {
                    results.AddRange(cachedReviews);
                }
                else
                {
                    nonCachedUsers.Add(id);
                }

            }
            var fromDbResults = await _reviewRepository.GetRecentReviewsForUsersAsync(nonCachedUsers, count);
            results.AddRange(_mapper.Map<IEnumerable<Review>, IEnumerable<ReviewDto>>(fromDbResults));
            return results; 
        }
        catch (Exception)
        {

            throw;
        }
    }
}
