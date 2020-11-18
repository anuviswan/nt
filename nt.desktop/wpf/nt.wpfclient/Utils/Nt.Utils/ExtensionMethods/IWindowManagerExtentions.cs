﻿using Caliburn.Micro;
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

        public static bool? ShowNtDialog(this IWindowManager source, NtViewModelBase viewModel,NtWindowSize windowModel,string title)
        {
            var (height, width) = windowModel switch
            {
                NtWindowSize.SmallLandscape => (300, 200),
                NtWindowSize.MediumLandscape => (400, 300),
                NtWindowSize.LargeLandscape => (500, 400),
                NtWindowSize.SmallPortrait => (200, 300),
                NtWindowSize.MediumPortrait => (300, 400),
                NtWindowSize.LargePortrait => (400,500),
                _ => (500,400)
            };

            return source.ShowNtDialog(viewModel, height,width,title);
        }

        private static bool? ShowNtDialog(this IWindowManager source,NtViewModelBase viewModel,double width,double height,string title)
        {
            dynamic settings = new ExpandoObject();
            settings.Height = height;
            settings.Width = width;
            settings.SizeToContent = SizeToContent.Manual;
            settings.Title = title;

            return source.ShowDialog(viewModel, null, settings);
        }

        
    }

    public class NtWindow : WindowManager
    {
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
        LargePortrait
    }
}
