using Nt.Utils.ControlInterfaces;

namespace Nt.Controls.Review
{
    public class ReviewViewModel : NtViewModelBase<ReviewControl>
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
        public string Author
        {
            get => TypedControl.Author?.DisplayName;
        }

        public string MovieTitle => TypedControl.Movie.Title;
        
    }
}
