using MahApps.Metro.Controls;
using Nt.WpfClient.ViewModels.Base;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Nt.WpfClient.Converters
{
    public class NtMenuItemViewModelToHamburgerMenuItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is NtMenuItemViewModelBase nmItem)
            {
                return new HamburgerMenuIconItem
                {
                    Tag = nmItem,
                    Icon = nmItem.Icon,
                    Label = nmItem.Title
                };
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
