﻿using System.Collections.Generic;

namespace Nt.Infrastructure.WebApi.ViewModels.Areas.Review.GetAllReviews
{
    public record GetAllReviewsResponse
    {
        public string MovieId { get; set; }
        public IEnumerable<ReviewItem> Reviews { get; set; }
    }

    public record ReviewItem
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public double Rating { get; set; }

        public string Description { get; set; }
        public string AuthorDisplayName { get; set; }
        public string AuthorUserName { get; set; }
        public string AuthorId { get; set; }

        public int AuthorFollowers { get; set; }
    }
}
