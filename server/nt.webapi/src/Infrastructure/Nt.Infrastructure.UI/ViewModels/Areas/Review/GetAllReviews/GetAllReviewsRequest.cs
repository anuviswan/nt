using System.ComponentModel.DataAnnotations;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetAllReviews;
public record GetAllReviewsRequest
{
    [Required(ErrorMessage = "Movie Id cannot be empty")]
    public string MovieId { get; set; }
}
