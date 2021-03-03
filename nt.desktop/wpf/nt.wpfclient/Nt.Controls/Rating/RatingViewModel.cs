using Nt.Utils.ControlInterfaces;

namespace Nt.Controls.Rating
{
    public class RatingViewModel : NtViewModelBase<RatingControl>
    {
        public int MaximumRating => TypedControl.MaximumRating;
        public int MinimumRating => TypedControl.MininumRating;
        public int Value => TypedControl.Value;
    }
}
