using System;
using System.Collections.Generic;
using System.Text;

namespace Nt.Shared.Utils.Helpers
{
    public static class IoC
    {
        public static Func<Type, object> GetInstance { get; set; }

        public static T Get<T>()
        {
            return (T)GetInstance(typeof(T));
        }
    }
}
