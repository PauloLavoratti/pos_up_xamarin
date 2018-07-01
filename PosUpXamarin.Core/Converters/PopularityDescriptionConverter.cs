using System;
using I18NPortable;
using Xamarin.Forms;

namespace PosUpXamarin.Core.Converters
{
    public class PopularityDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var popularityLabel = "Popularity".Translate();

            if (value == null) 
            {
                return $"{popularityLabel} {0:P2}";
            }

            return $"{popularityLabel} {((double)value / 100):P2}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
