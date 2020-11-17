using Caliburn.Micro;
using MahApps.Metro.Controls;
using Nt.Utils.ControlInterfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Nt.Utils.ExtensionMethods
{
    public static class IWindowManagerExtentions
    {

        public static bool? ShowNtDialog(this IWindowManager source, object context,  NtViewModelBase viewModel,NtWindowSize windowModel)
        {
            var (height, width) = windowModel switch
            {
                NtWindowSize.SmallLandscape => (300, 200),
                NtWindowSize.MediumLandscape => (400, 300),
                _ => (500,400)
            };

            return source.ShowNtDialog(context, viewModel, height,width);
        }

        private static bool? ShowNtDialog(this IWindowManager source,object context, NtViewModelBase viewModel,double width,double height)
        {

            dynamic settings = new ExpandoObject();
            settings.Height = height;
            settings.Width = width;
            settings.SizeToContent = SizeToContent.Manual;

            var ntWindow = (NtWindow)source;
            var windowInstance = ntWindow.CreateWindowInternal(viewModel, true, context, settings);
            return windowInstance.ShowDialog();
        }

        
    }

    public class NtWindow : WindowManager
    {
        public Window CreateWindowInternal(object rootModel, bool isDialog, object context, IDictionary<string, object> settings)
        {
            return CreateWindow(rootModel, isDialog, context, settings);
        }

        protected override Window EnsureWindow(object model, object view, bool isDialog)
        {
            var window = new MetroWindow()
            {
                Content = view
            };
            return window;
        }

    }
    public enum NtWindowSize
    {
        SmallLandscape,
        MediumLandscape,
        LargeLandscape,

        SmallPortrait,
        MediumPortrait,
        LargePrtrait
    }
}
