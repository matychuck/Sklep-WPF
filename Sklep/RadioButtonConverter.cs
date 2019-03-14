using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sklep
{
     public class RadioButtonConverter : IValueConverter // do konwersji radiobttona kobiety(0)  i mezczyzny(1)
     {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                int integer = (int)value;
                if (integer == int.Parse(parameter.ToString()))
                    return true;
                else
                    return false;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return parameter;
            }

        
     }
}
