using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Metro.IconPacks;
using Nt.Controls.Review;
using Nt.Utils.ServiceInterfaces;
using Nt.WpfClient.ViewModels.Base;

namespace Nt.WpfClient.ViewModels
{
    public class HomeViewModel : NtMenuItemViewModelBase
    {
        public override string Title => "Home";
        public override object Icon => new PackIconMaterial { Kind = PackIconMaterialKind.Home};
        
        public IEnumerable<ReviewViewModel> Reviews { get; set; } = Enumerable.Empty<ReviewViewModel>();

        public ReviewViewModel ReviewItem { get; set; }
        protected override async void OnViewLoaded(object view)
        {
            await base.OnViewAttached();
            var movieService = IoC.Get<IMovieService>();
            var reviews = await movieService.GetRecentReviews();

            Reviews = reviews.Select(x => new ReviewControl
            {
                Description = x.Description,
                Rating = x.Rating,
                Title = x.Title,
            }.ViewModel).ToList();


            ReviewItem = Reviews.First();

            NotifyOfPropertyChange(nameof(ReviewItem));
        }
    }
}
