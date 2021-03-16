using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nt.Controls.Pages.Login;
using Nt.Shared.Utils.ControlBase;

namespace Nt.Desktop.Bootstrap
{
    public static class ViewModelLoader
    {
        public static IEnumerable<Assembly> GetAssemblies() => new Assembly[] { typeof(LoginControl).Assembly };
        public static IEnumerable<Type> GetViewModels()
        {
            var controls = typeof(LoginControl).Assembly
                            .GetTypes()
                            .Where(x => IsSubclassOfRawGeneric(typeof(ViewModelBase<>), x));

            var viewModels = controls.Where(x => x.BaseType.GetGenericArguments().Any())
                            .Select(x => x.BaseType.GetGenericArguments().First());
            return viewModels;
        }

        private static object CreateGenericControl(Type typeArguement)
        {
            var d1 = typeof(ViewModelBase<>);
            Type[] typeArgs = { typeArguement };
            var makeme = d1.MakeGenericType(typeArgs);
            return Activator.CreateInstance(makeme);
        }

        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}
