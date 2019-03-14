using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace Sklep
{
    class PriceGrouper : IValueConverter
    {
        public int GroupInterval { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal price = (decimal)value;
            if (price < GroupInterval)
            {
                return String.Format(culture, "Mniej niż {0:C}",
                GroupInterval);
            }
            else
            {
                int interval = (int)price / GroupInterval;
                int lowerLimit = interval * GroupInterval;
                int upperLimit = (interval + 1) * GroupInterval;
                return String.Format(culture, "{0:C} – {1:C}", lowerLimit, upperLimit);
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

