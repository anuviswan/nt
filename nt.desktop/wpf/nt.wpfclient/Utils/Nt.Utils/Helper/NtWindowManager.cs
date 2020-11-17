using Caliburn.Micro;
using MahApps.Metro.Controls;
using Nt.Utils.ControlInterfaces;
using Nt.Utils.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nt.Utils.Helper
{
    public class NtWindowManager: WindowManager
    {
        protected override Window EnsureWindow(object model, object view, bool isDialog)
        {
            if (view == null) return default;

            if (view is MetroWindow metroWindow) return metroWindow;

            var window = new MetroWindow
            {
                Content = view,
                SizeToContent = SizeToContent.WidthAndHeight
            };
            window.SetValue(View.IsGeneratedProperty, true);
            var owner = InferOwnerOf(window);

            if(owner!=null)
            window.Owner = owner;
            return window;
        }
    }
}
