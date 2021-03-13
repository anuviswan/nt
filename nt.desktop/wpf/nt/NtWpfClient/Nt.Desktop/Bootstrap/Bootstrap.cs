using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Nt.Desktop.ViewModels;
using Nt.Desktop.Views;
using Unity;

namespace Nt.Desktop.Bootstrap
{
    public class Bootstrap:BootstrapBase
    {
        IUnityContainer container = new UnityContainer();
        public Bootstrap()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);

            ShellView shellView = new ShellView();
            shellView.DataContext = new ShellViewModel();
            shellView.Show();
        }
    }
}
