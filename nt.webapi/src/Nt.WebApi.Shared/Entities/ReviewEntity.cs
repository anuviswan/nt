using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.WebApi.Shared.Entities
{
    public class ReviewEntity : BaseEntity
    {
        public int MovieId { get; set; }
        public string ReviewTitle { get; set; }
        public string ReviewDescription { get; set; }
    }
}
