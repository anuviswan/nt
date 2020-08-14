using Nt.Controls.Login;
using Nt.Utils.ControlInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nt.WpfClient.Utils.Bootstrap
{
    public static class ViewModelLoader
    {
        public static IEnumerable<Assembly> GetAssemblies()=>new Assembly[] { typeof(LoginControl).Assembly};
        public static IEnumerable<NtViewModelBase> GetViewModels() => typeof(LoginControl).Assembly
                .GetTypes()
                .Where(x => typeof(NtControlBase).IsAssignableFrom(x))
                .Select(x => Activator.CreateInstance(x)).Cast<NtControlBase>().Select(x => x.ViewModel);
    }
}
