using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Gametron.Converters
{
    class SortDescriptionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SortDescription sort = (SortDescription)value;
            return sort.PropertyName == parameter.ToString() ? (sort.Direction == ListSortDirection.Ascending ? "▴" : "▾") : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as string) switch
            {
                "▴" => new SortDescription(parameter.ToString(), ListSortDirection.Ascending),
                "▾" => new SortDescription(parameter.ToString(), ListSortDirection.Descending),
                _ => Binding.DoNothing,
            };
        }
    }
}
