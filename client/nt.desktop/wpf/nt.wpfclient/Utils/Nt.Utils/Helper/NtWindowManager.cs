using Caliburn.Micro;
using MahApps.Metro.Controls;
using System.Windows;

namespace Nt.Utils.Helper;

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
