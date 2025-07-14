using ReviewService.Infrastructure.Repository.Documents;

namespace ReviewService.Infrastructure.Repository.Seed;

public static class MalayalamReviewsSeed
{
    public static IEnumerable<ReviewDocument> Reviews => [];
}

public static class Seed
{
    public static IEnumerable<ReviewDocument> MalayalamReviews => MalayalamReviewsSeed.Reviews;
    
}
