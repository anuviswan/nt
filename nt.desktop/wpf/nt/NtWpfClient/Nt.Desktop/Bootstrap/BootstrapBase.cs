using System;
using System.Linq;
using System.Windows;
using Nt.Desktop.Views;
using Nt.Shared.Utils.ControlBase;
using Nt.Shared.Utils.ServiceInterfaces;

namespace Nt.Desktop.Bootstrap
{
    public abstract class BootstrapBase
    {
        bool isInitialized;
        public void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;
            PrepareApplication();
            Configure();
        }

        protected Application Application { get; set; }

        protected virtual object GetInstance(Type type)
        {
            return default;
        }

        protected virtual void Configure()
        {

        }
        protected void PrepareApplication()
        {
            Application = Application.Current;
            Application.Startup += OnStartup;

            IoC.GetInstance = GetInstance;
        }

        protected virtual void OnStartup(object sender, StartupEventArgs e)
        {
        }

        protected void OpenRoot<TViewModel>() where TViewModel:ViewModelBase
        {
            var instance = IoC.Get<TViewModel>();
            var windowService = IoC.Get<IWindowService>();

            windowService.ShowDialog(instance,null);

           // windowService.ShowDialog(instance);
        }

       
    }
}
