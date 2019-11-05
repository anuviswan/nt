using Nt.WebApi.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.WebApi.Tests.Controller
{
    public class ReviewControllerTests : BaseControllerTests<ReviewEntity>
    {
        private void InitliazeCollection()
        {
            EntityCollection = Enumerable.Range(1, 10).Select(x => new ReviewEntity
            {
                MovieId = 1,
                ReviewTitle = $"Title {x}",
                ReviewDescription = $"Description {x}",
            }).ToList();
        }



    }
}
