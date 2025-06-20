using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewService.Application.DTO.Reviews;

public class  Review
{
    public Guid ReviewId { get; set; }
    public string MovieTitle { get; set; }
}
