using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Nt.Desktop.ViewModels;
using Nt.Desktop.Views;
using Nt.Shared.Utils.ServiceInterfaces;
using Nt.Shared.Utils.Services;
using Unity;

namespace Nt.Desktop.Bootstrap
{
    public class Bootstrap:BootstrapBase
    {
        IUnityContainer _container = new UnityContainer();
        public Bootstrap()
        {
            Initialize();
        }

        protected override object GetInstance(Type type)
        {
            return _container.Resolve(type);
        }

        protected override void Configure()
        {
            base.Configure();
            _container.RegisterInstance<IWindowService>(new WindowService());
            _container.RegisterInstance<ShellViewModel>(new ShellViewModel());

            foreach (var vmTypes in ViewModelLoader.GetViewModels())
            {
                _container.RegisterType(vmTypes);
            }
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);
            OpenRoot<ShellViewModel>();
            //ShellView shellView = new ShellView();
            //shellView.DataContext = new ShellViewModel();
            //shellView.Show();
        }
    }
}
