using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Application.Interfaces.Operations;

public interface IReviewService
{
    public Task GetReview(string movieId);
}
