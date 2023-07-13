using Nt.Controls.UserProfile;
using Nt.Utils.ControlInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nt.WpfClient.Utils.Bootstrap;

public static class ViewModelLoader
{
    public static IEnumerable<Assembly> GetAssemblies()=>new Assembly[] { typeof(UserProfileControl).Assembly};
    public static IEnumerable<Type> GetViewModels()
    {
        var controls= typeof(UserProfileControl).Assembly
                        .GetTypes()
                        .Where(x => IsSubclassOfRawGeneric(typeof(NtControlBase<>), x));
        
        var viewModels = controls.Where(x => x.BaseType.GetGenericArguments().Any())
                        .Select(x=> x.BaseType.GetGenericArguments().First());
        return viewModels;
    }

    private static object CreateGenericControl(Type typeArguement)
    {
        var d1 = typeof(NtControlBase<>);
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
