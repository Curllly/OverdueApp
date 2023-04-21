using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace OverdueMobileDesktop.Converters
{
    public class PlaceIdToPlaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case 1:
                    return "Мясной холодильник";
                case 2:
                    return "Молочный холодильник";
                case 3:
                    return "Бакалея консервация";
                case 4:
                    return "Сладкая бакалея";
                case 5:
                    return "Нотфуд";
                case 6:
                    return "Морозилка";
                default:
                    return "Место хранения не обозначено";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
