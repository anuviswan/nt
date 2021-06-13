using MahApps.Metro.Controls;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Nt.Desktop.Converters
{
    public class MenuItemToPageViewModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is HamburgerMenuIconItem menuItem ? menuItem.Tag : value;
        }
    }
}
