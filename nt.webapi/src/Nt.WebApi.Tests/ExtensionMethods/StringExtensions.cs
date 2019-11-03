using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nt.WebApi.Tests.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string ChangeCase(this string source)
        {
            return new string(source.Select(c => char.IsLetter(c) ? char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c) : c).ToArray());
        }

    }
}
