using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using MahApps.Metro.Controls;
using Nt.Shared.Utils.ControlBase;

namespace Nt.Desktop.Converters
{
    public class PageCollectionToMenuItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is IEnumerable<MenuItemViewModelBase> nmItemCollection)
            {
                return nmItemCollection.Select(item => new HamburgerMenuIconItem
                {
                    Tag = item,
                    Icon = item.Icon,
                    Label = item.Name
                });
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
