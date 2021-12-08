using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nt.Shared.Utils.ServiceInterfaces;
using Nt.Shared.Utils.Services;
using Unity;

namespace Nt.Desktop.Bootstrap
{
    public class ServiceManager
    {
        public static void RegisterServices(IUnityContainer container)
        {
            container.RegisterSingleton<IWindowService, WindowService>();
            container.RegisterSingleton<IUserService, UserService>();
            container.RegisterType<IHttpService, HttpService>();
            container.RegisterSingleton<IEventAggregator, EventAggregator>();
        }
    }
}
