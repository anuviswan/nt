using MahApps.Metro.Controls;
using Nt.WpfClient.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Nt.WpfClient.Converters;

public class PageCollectionToMenuItemConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is IEnumerable<NtMenuItemViewModelBase> nmItemCollection)
        {
            return nmItemCollection.Select(item => new HamburgerMenuIconItem
            {
                Tag = item,
                Icon = item.Icon,
                Label = item.Title
            });
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
