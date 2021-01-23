using System;
using System.Collections.Generic;
using System.Text;
using Nt.Data.Dto.BaseDto;

namespace Nt.Data.Dto.GetRecentReviews
{
    public class RecentReviewsRequest:BaseRequest
    {
        public int NumberOfItems { get; set; }
    }
}
