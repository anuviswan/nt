using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Nt.Desktop.ViewModels;
using Nt.Desktop.Views;
using Nt.Shared.Utils.ServiceInterfaces;
using Nt.Shared.Utils.Services;
using Unity;
using Unity.Injection;

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
            ServiceManager.RegisterServices(_container);
            _container.RegisterType<ShellViewModel>(new InjectionConstructor(_container.Resolve<IWindowService>(), DialogCoordinator.Instance));
           // _container.RegisterType<ShellViewModel>();

            foreach (var vmTypes in ViewModelLoader.GetViewModels())
            {
                _container.RegisterType(vmTypes);
            }
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);
            OpenRoot<ShellViewModel>();
       
        }
    }
}
