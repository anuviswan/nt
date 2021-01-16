using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Domain.Entities.Movie;

namespace Nt.Infrastructure.Tests.Helpers.TestData
{
    public static class Reviews
    {
        public static IEnumerable<ReviewEntity> GetReviewCollection()
        {
            yield return new ReviewEntity
            {
                Id = "R1",
                MovieId = string.Format(Utils.MockMovieIdFormat, 1),
                AuthorId = string.Format(Utils.MockUserIdFormat, 1),
                ReviewTitle = $"{nameof(ReviewEntity.ReviewTitle)} 01",
                ReviewDescription = $"{nameof(ReviewEntity.ReviewDescription)} 01",
                DownVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 2),
                    string.Format(Utils.MockUserIdFormat, 3),
                    string.Format(Utils.MockUserIdFormat, 4)
                },
                UpVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 5),
                    string.Format(Utils.MockUserIdFormat, 6)
                },
                Rating = 4,
                CreatedOn = new DateTime(2021, 1, 20)
            };
            yield return new ReviewEntity
            {
                Id = "R2",
                MovieId = string.Format(Utils.MockMovieIdFormat, 1),
                AuthorId = string.Format(Utils.MockUserIdFormat, 7),
                ReviewTitle = $"{nameof(ReviewEntity.ReviewTitle)} 02",
                ReviewDescription = $"{nameof(ReviewEntity.ReviewDescription)} 02",
                DownVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 2),
                    string.Format(Utils.MockUserIdFormat, 3),
                    string.Format(Utils.MockUserIdFormat, 4)
                },
                UpVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 1),
                    string.Format(Utils.MockUserIdFormat, 5),
                    string.Format(Utils.MockUserIdFormat, 6)
                },
                Rating = 4,
                CreatedOn = new DateTime(2021, 1, 18)
            };
            yield return new ReviewEntity
            {
                Id = "R3",
                MovieId = string.Format(Utils.MockMovieIdFormat, 2),
                AuthorId = string.Format(Utils.MockUserIdFormat, 7),
                ReviewTitle = $"{nameof(ReviewEntity.ReviewTitle)} 01",
                ReviewDescription = $"{nameof(ReviewEntity.ReviewDescription)} 01",
                DownVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 2),
                    string.Format(Utils.MockUserIdFormat, 3),
                    string.Format(Utils.MockUserIdFormat, 4)
                },
                UpVotedBy = new List<string>
                {
                    string.Format(Utils.MockUserIdFormat, 1),
                    string.Format(Utils.MockUserIdFormat, 5),
                    string.Format(Utils.MockUserIdFormat, 6)
                },
                Rating = 3,
                CreatedOn = new DateTime(2021, 1, 16)
            };
        }
    }
}
