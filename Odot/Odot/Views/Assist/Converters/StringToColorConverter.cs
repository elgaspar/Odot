using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Odot.Views.Assist.Converters
{
    [ValueConversion(typeof(string), typeof(Brush))]
    class StringToColorConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException("The target must be Brush.");

            string color = (string)value;
            if (color == "None")
                return Brushes.Black;
            if (color == "Blue")
                return Brushes.Blue;
            if (color == "Gray")
                return Brushes.Gray;
            if (color == "Green")
                return Brushes.Green;
            if (color == "Orange")
                return Brushes.Orange;
            if (color == "Red")
                return Brushes.Red;
            if (color == "Yellow")
                return Brushes.Yellow;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
