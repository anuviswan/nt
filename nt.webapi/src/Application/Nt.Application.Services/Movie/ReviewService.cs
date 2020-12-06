using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Domain.Entities.Exceptions;
using Nt.Domain.Entities.Movie;
using Nt.Domain.RepositoryContracts;
using Nt.Domain.ServiceContracts.Movie;

namespace Nt.Application.Services.Movie
{
    public class ReviewService : ServiceBase, IReviewService
    {
        public ReviewService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<ReviewEntity> CreateAsync(ReviewEntity review,string authorUserName)
        {
            var user = await UnitOfWork.UserProfileRepository.GetAsync(x => x.UserName.ToLower() == authorUserName.ToLower() && x.IsDeleted == false);
            var userID = user.Single().Id;
            review = new ReviewEntity() with { AuthorId = userID };

            var existingReview = await UnitOfWork.ReviewRepository
                .GetAsync(x => x.MovieId == review.MovieId && x.AuthorId == review.AuthorId)
                .ConfigureAwait(false);

            if (existingReview.Any())
            {
                throw new EntityAlreadyExistException();
            }

            var result = await UnitOfWork.ReviewRepository.CreateAsync(review).ConfigureAwait(false);
            return result;
        }
    }
}
