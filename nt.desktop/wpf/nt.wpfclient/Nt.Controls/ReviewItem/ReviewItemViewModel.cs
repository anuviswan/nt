using Nt.Utils.ControlInterfaces;

namespace Nt.Controls.ReviewItem
{
    public class ReviewItemViewModel: NtViewModelBase<ReviewItemControl>
    {
        public string Title 
        {
            get => TypedControl.Title;
            set => TypedControl.Title = value;
        }

        public string Description
        {
            get => TypedControl.Description;
            set => TypedControl.Description = value;
        }

        public double Rating
        {
            get => TypedControl.Rating;
            set => TypedControl.Rating = value;
        }

    }
}
