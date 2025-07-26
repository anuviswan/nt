namespace ReviewService.Presenation.Api.Models;

public record GetRecentReviewsForUsersRequest(IEnumerable<string> UserIds, int Count = 3);
