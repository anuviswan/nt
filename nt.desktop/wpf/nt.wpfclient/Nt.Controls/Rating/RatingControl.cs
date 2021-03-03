using Nt.Utils.ControlInterfaces;

namespace Nt.Controls.Rating
{
    public class RatingControl : NtControlBase<RatingViewModel>
    {
        public int MininumRating { get; set; } = 0;
        public int MaximumRating { get; set; } = 5;
        public int Value { get; set; }
    }
}
