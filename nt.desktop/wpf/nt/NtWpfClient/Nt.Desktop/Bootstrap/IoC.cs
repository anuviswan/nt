using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nt.Desktop.Bootstrap
{
    public static class IoC
    {
        public static Func<Type,object> GetInstance { get; set; }

        public static T Get<T>()
        {
            return (T)GetInstance(typeof(T));
        }
    }
}
