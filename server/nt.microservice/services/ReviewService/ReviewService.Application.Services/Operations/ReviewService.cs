using AutoMapper;
using Microsoft.Extensions.Logging;
using ReviewService.Application.DTO.Reviews;
using ReviewService.Application.Interfaces.Operations;
using ReviewService.Domain.Entities;
using ReviewService.Domain.Repositories;

namespace ReviewService.Application.Services.Operations;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    public ReviewService(IReviewRepository reviewRepository,IMapper mapper, ILogger<ReviewService> logger)
    {
        _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
}
