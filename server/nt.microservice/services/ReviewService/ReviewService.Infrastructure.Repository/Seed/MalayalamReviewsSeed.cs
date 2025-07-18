using ReviewService.Infrastructure.Repository.Documents;

namespace ReviewService.Infrastructure.Repository.Seed;

public static class MalayalamReviewsSeed
{
    public static IEnumerable<ReviewDocument> Reviews => [
        new(){
            Content = "Super movie",
            ID  = Guid.Parse("d1f8c2b0-3e4b-4c5a-9f6b-7a8b9c0d1e2f").ToString(),
            MovieId = Guid.Parse("6191e634-14c8-45d1-898f-191060cdbec1"),
            Rating = 5,
            ReviewId = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
            Title = "Super Movie",
            UserName = "jia.anu"
        }
        ];
}
