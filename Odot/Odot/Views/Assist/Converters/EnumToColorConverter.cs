using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Odot.Views.Assist.Converters
{
    [ValueConversion(typeof(Models.Colors), typeof(Brush))]
    class EnumToColorConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException("The target must be Brush.");

            Models.Colors color = (Models.Colors)value;
            return EnumToColor(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static Brush EnumToColor(Models.Colors color)
        {
            if (color == Models.Colors.None)
                return Brushes.Black;
            if (color == Models.Colors.Blue)
                return Brushes.Blue;
            if (color == Models.Colors.Gray)
                return Brushes.Gray;
            if (color == Models.Colors.Green)
                return Brushes.Green;
            if (color == Models.Colors.Orange)
                return Brushes.Orange;
            if (color == Models.Colors.Red)
                return Brushes.Red;
            if (color == Models.Colors.Yellow)
                return Brushes.Yellow;
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
