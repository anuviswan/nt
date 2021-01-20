using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MahApps.Metro.IconPacks;
using Nt.Controls.ReviewItem;
using Nt.Utils.ServiceInterfaces;
using Nt.WpfClient.ViewModels.Base;

namespace Nt.WpfClient.ViewModels
{
    public class HomeViewModel : NtMenuItemViewModelBase
    {
        public override string Title => "Home";
        public override object Icon => new PackIconMaterial { Kind = PackIconMaterialKind.Home};
        
        public IEnumerable<ReviewItemViewModel> Reviews { get; set; } = Enumerable.Empty<ReviewItemViewModel>();

        protected override async Task OnViewAttached()
        {
            await base.OnViewAttached();
            var movieService = IoC.Get<IMovieService>();
            var reviews = await movieService.GetRecentReviews();

            reviews.Select(x => new ReviewItemViewModel
            {
                Description = x.Description,
                Rating = x.Rating,
                Title = x.Title,
            });
        }
    }
}
