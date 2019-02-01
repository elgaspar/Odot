using System.Windows;
using System.Windows.Controls;

namespace Odot.Views
{
    /// <summary>
    /// Interaction logic for HyperlinkButton.xaml
    /// </summary>
    public partial class HyperlinkButton : Button
    {
        public HyperlinkButton()
        {
            InitializeComponent();
        }



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(HyperlinkButton), new PropertyMetadata(string.Empty));


    }
}
