using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Odot.Views.Assist.Converters
{
    class AddEditDialogTitleConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = (string)parameter;
            if ((bool)value)
                return "Add " + param;
            else
                return "Edit " + param;
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
