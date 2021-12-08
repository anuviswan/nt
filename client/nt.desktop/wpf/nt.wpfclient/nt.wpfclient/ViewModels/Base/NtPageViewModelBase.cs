using System.Threading.Tasks;
using Caliburn.Micro;

namespace Nt.WpfClient.ViewModels.Base
{
    public class NtPageViewModelBase:Screen
    {
        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            Task.Run(OnViewAttached);
        }

        protected virtual Task OnViewAttached()
        {
            return Task.CompletedTask;
        }
    }
}
