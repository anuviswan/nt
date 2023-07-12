using Nt.Utils.ControlInterfaces;
using Nt.Utils.Models;

namespace Nt.Controls.Review;

public class ReviewControl : NtControlBase<ReviewViewModel>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; }
    public User Author { get; set; }
    public Movie Movie { get; set; }
}
