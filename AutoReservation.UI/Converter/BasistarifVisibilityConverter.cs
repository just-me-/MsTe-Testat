using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.UI.Converter
{
    class BasistarifVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AutoKlasse klasse = (AutoKlasse)value;
            return (klasse == AutoKlasse.Luxusklasse) ?  Visibility.Visible : Visibility.Collapsed ;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Code should not be reached");
        }
    }
}
