using ReviewService.Infrastructure.Repository.Documents;

namespace ReviewService.Infrastructure.Repository.Seed;

public static class MalayalamReviewsSeed
{
    public static IEnumerable<ReviewDocument> Reviews => [
        new() {
        Content = "A heartwarming tale with mouthwatering visuals and a soulful story. Loved it!",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("6191e634-14c8-45d1-898f-191060cdbec1"),
        Rating = 5,
        Title = "Ustad Hotel Feels",
        Author = "jia.anu",
        UpvotedBy = ["naina.anu","jia.anu"],
    },
    new() {
        Content = "Dulquer and Thilakan make this movie a beautiful emotional ride. Music was perfect.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("6191e634-14c8-45d1-898f-191060cdbec1"),
        Rating = 4,
        Title = "Beautiful Blend",
        Author = "jia.anu",
        UpvotedBy = ["sreena.anu"],
    },
    new() {
        Content = "Joji is an intense, slow burn thriller. Brilliant acting by Fahadh as always.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("bea50cb7-41b6-4a35-b77f-358e8c43f850"),
        Rating = 4,
        Title = "Dark and Gripping",
        Author = "naina.anu"
    },
    new() {
        Content = "Minimal dialogues, maximum impact. Joji is a masterclass in modern Malayalam cinema.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("bea50cb7-41b6-4a35-b77f-358e8c43f850"),
        Rating = 5,
        Title = "Minimalist Brilliance",
        Author = "jia.anu"
    },
    new() {
        Content = "A hilarious ride with unexpected twists. Oru Vadakkan Selfie is pure fun.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("bd4b80a0-1516-4b71-887b-714c52459f23"),
        Rating = 4,
        Title = "Comedy Hit",
        Author = "naina.anu"
    },
    new() {
        Content = "Selfie gone wrong but in the best way possible. Loved the storytelling and humor.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("bd4b80a0-1516-4b71-887b-714c52459f23"),
        Rating = 5,
        Title = "Smart Comedy",
        Author = "jia.anu",
        DownvotedBy = ["jia.anu"],
        UpvotedBy = ["naina.anu"]
    },
    new() {
        Content = "Malik is powerful. A political saga with gripping performances. Fahadh nailed it!",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("0003be11-f19a-4b9c-a1b2-b7e195b53d3e"),
        Rating = 5,
        Title = "Political Power",
        Author = "naina.anu"
    },
    new() {
        Content = "One of the best performances by Fahadh. Malik stays with you long after it ends.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("0003be11-f19a-4b9c-a1b2-b7e195b53d3e"),
        Rating = 5,
        Title = "Brilliant Execution",
        Author = "jia.anu",
        UpvotedBy = ["jia.anu", "sreena.anu", "naina.anu"]
    },
    new() {
        Content = "Premam is nostalgic, fun, and full of charm. Every phase of love was shown beautifully.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("af3e9bed-3e04-4f06-856d-3c572605bf4d"),
        Rating = 5,
        Title = "Love Story Goals",
        Author = "naina.anu",
        UpvotedBy = ["jia.anu", "sreena.anu", "naina.anu"]
    },
    new() {
        Content = "Great music, wonderful performances. Premam is a modern Malayalam classic.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("af3e9bed-3e04-4f06-856d-3c572605bf4d"),
        Rating = 5,
        Title = "Evergreen",
        Author = "jia.anu"
    },
    new() {
        Content = "Jana Gana Mana is thought-provoking. Raises valid questions about justice and media.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("47435a1d-6b24-4cfc-b3a6-0be543322187"),
        Rating = 4,
        Title = "Relevant and Bold",
        Author = "naina.anu"
    },
    new() {
        Content = "Powerful script and solid performances. Worth watching more than once.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("47435a1d-6b24-4cfc-b3a6-0be543322187"),
        Rating = 5,
        Title = "Must Watch",
        Author = "jia.anu"
    },
    new() {
        Content = "Ustad Hotel remains a comfort movie. It warms the soul and stirs the appetite.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("6191e634-14c8-45d1-898f-191060cdbec1"),
        Rating = 5,
        Title = "Feel-Good Watch",
        Author = "naina.anu"
    },
    new() {
        Content = "Malik's political layers and gripping visuals make it a standout Malayalam film.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("0003be11-f19a-4b9c-a1b2-b7e195b53d3e"),
        Rating = 4,
        Title = "Strong Narrative",
        Author = "malayalicritic"
    },
    new() {
        Content = "Premam redefined romance in Malayalam cinema. A perfect blend of nostalgia and music.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("af3e9bed-3e04-4f06-856d-3c572605bf4d"),
        Rating = 5,
        Title = "Romantic Classic",
        Author = "cinepulse"
    },
    new() {
        Content = "Joji’s silence speaks louder than words. A chilling adaptation of Macbeth.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("bea50cb7-41b6-4a35-b77f-358e8c43f850"),
        Rating = 4,
        Title = "Psychological Drama",
        Author = "joji_fan"
    },
    new() {
        Content = "Loved every bit of Oru Vadakkan Selfie. Light-hearted and perfect for a rewatch!",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("bd4b80a0-1516-4b71-887b-714c52459f23"),
        Rating = 4,
        Title = "Laugh Riot",
        Author = "minnal_m"
    },
    new() {
        Content = "Jana Gana Mana deserves more attention. Thought-provoking and very well directed.",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("47435a1d-6b24-4cfc-b3a6-0be543322187"),
        Rating = 5,
        Title = "Social Eye-Opener",
        Author = "vocalviewer"
    },
    new() {
        Content = "Premam’s characters grow on you. Sai Pallavi was just magical!",
        ID = Guid.NewGuid().ToString(),
        MovieId = Guid.Parse("af3e9bed-3e04-4f06-856d-3c572605bf4d"),
        Rating = 5,
        Title = "Romance and Charm",
        Author = "sai_pallavi_fan"
    }
    ];
}
