using Nt.Controls.Login;
using Nt.Utils.ControlInterfaces;
using System;
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
                .Where(x => typeof(NtControlBase<>).IsAssignableFrom(x))
                .Select(x => x.GetGenericArguments().First()).Select(x=> (NtViewModelBase)Activator.CreateInstance(x));

        private static object CreateGenericControl(Type typeArguement)
        {
            var d1 = typeof(NtControlBase<>);
            Type[] typeArgs = { typeArguement };
            var makeme = d1.MakeGenericType(typeArgs);
            return Activator.CreateInstance(makeme);
        }
    }
}
