using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Nt.Controls.HelperControls.WindowManager;
using Nt.Shared.Utils.ControlBase;
using Nt.Shared.Utils.ServiceInterfaces;

namespace Nt.Shared.Utils.Services
{
    public class WindowService : IWindowService
    {
        public IList<Type> KnownTypes { get; set; }
        public bool? ShowDialog(ViewModelBase viewModel)
        {
            var viewModelType = viewModel.GetType();
            var view = GetViewForViewModel(viewModelType);
            if (view is Window win)
            {
                win.DataContext = viewModel;
                return win.ShowDialog();
            }
            else
            {
                var windowDialog = new Window();
                windowDialog.Title = "Demo";
                return windowDialog.ShowDialog();
            }
            
        }

        private object GetViewForViewModel(Type type)
        {
            var viewName = type.Name[..^5];
            var view = type.Assembly.GetTypes().Where(x => string.Equals(x.Name, viewName));
            if (view.Any())
            {
                var v = view.First();
                var viewInstance = Activator.CreateInstance(v);
                return viewInstance;
            }
            return null;
        }
    }
}
