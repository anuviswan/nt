using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Review.CreateReview
{
    public record CreateReviewRequest
    {
        [Required(ErrorMessage = "Movie Id cannot be empty")]
        public string MovieId { get; init; }
        [Required(ErrorMessage = "Title cannot be empty")]
        public string Title { get; init; }
        [Required(ErrorMessage = "Description cannot be empty")]
        [MaxLength(200, ErrorMessage = "Review cannot be more than 200 characters")]
        public string Description { get; init; }
        public double Rating { get; init; }
    }
}
