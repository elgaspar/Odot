using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Odot.Views.Assist.Converters
{
    class AddEditDialogIconConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string param = (string)parameter;
            string resourceName;
            if ((bool)value) //Add
                resourceName = param + "AddIconPath";
            else //Edit
                resourceName = param + "EditIconPath";
            return Application.Current.TryFindResource(resourceName) as string;
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
