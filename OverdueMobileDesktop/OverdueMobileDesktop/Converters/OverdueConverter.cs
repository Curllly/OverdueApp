using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace OverdueMobileDesktop.Converters
{
    public class OverdueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime bestbefore = (DateTime)value;
            int daysbetween = bestbefore.Subtract(DateTime.Now.Date).Days + 1;
            if (daysbetween <= 1)
                return "#900020";
            else if (daysbetween == 2)
                return "#FCE883";
            return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
