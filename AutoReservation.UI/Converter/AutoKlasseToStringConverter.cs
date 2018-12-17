using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.UI.Converter
{
    class AutoKlasseToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            AutoKlasse klasse = (AutoKlasse) value;
            switch (klasse)
            {
                case AutoKlasse.Standard:
                    return "Standard";
                case AutoKlasse.Mittelklasse:
                    return "Mittelklasse";
                case AutoKlasse.Luxusklasse:
                    return "Luxusklasse";
                default:
                    throw new ArgumentException("Autoklasse received first argument that is no known Autoklasse");
            }

           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Code should not be reached");
        }
    }
}
