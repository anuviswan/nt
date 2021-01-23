using Nt.Utils.ServiceInterfaces;
using Nt.Utils.Services;
using Unity;

namespace Nt.WpfClient.Utils.Bootstrap
{
    public static class ServiceLoader
    {
        public static void RegisterServices(IUnityContainer unityContainer)
        {
            unityContainer.RegisterSingleton<ICurrentUserService,CurrentUserService>();
            unityContainer.RegisterSingleton<IHttpService, HttpService>();
            unityContainer.RegisterSingleton<IMovieService, MovieService>();
        }
    }
}
