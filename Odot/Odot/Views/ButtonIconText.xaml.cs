using System.Windows;
using System.Windows.Controls;

namespace Odot.Views
{
    /// <summary>
    /// Interaction logic for ButtonIconText.xaml
    /// </summary>
    public partial class ButtonIconText : Button
    {
        public ButtonIconText()
        {
            InitializeComponent();
        }


        public string IconPath
        {
            get { return (string)GetValue(IconPathProperty); }
            set { SetValue(IconPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconPathProperty =
            DependencyProperty.Register("IconPath", typeof(string), typeof(ButtonIconText), new PropertyMetadata(string.Empty));


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ButtonIconText), new PropertyMetadata(string.Empty));

    }
}
