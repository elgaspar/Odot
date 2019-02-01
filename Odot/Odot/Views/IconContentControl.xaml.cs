using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Odot.Views
{
    /// <summary>
    /// Interaction logic for ContentControl.xaml
    /// </summary>
    public partial class IconContentControl : ContentControl
    {
        private const double DEFAULT_WIDTH_HEIGHT = 38;

        public IconContentControl()
        {
            InitializeComponent();
        }

        private Brush savedForegroundColor;


        public string DataPath
        {
            get { return (string)GetValue(DataPathProperty); }
            set { SetValue(DataPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataPathProperty =
            DependencyProperty.Register("DataPath", typeof(string), typeof(IconContentControl), new PropertyMetadata(string.Empty));


        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(IconContentControl), new PropertyMetadata(DEFAULT_WIDTH_HEIGHT));


        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IconHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(IconContentControl), new PropertyMetadata(DEFAULT_WIDTH_HEIGHT));



        public Brush ForegroundColor
        {
            get { return (Brush)GetValue(ForegroundColorProperty); }
            set { SetValue(ForegroundColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForegroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundColorProperty =
            DependencyProperty.Register("ForegroundColor", typeof(Brush), typeof(IconContentControl), new PropertyMetadata(Brushes.Black));

        private void ct_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsEnabled)
            {
                ForegroundColor = savedForegroundColor;
            }
            else
            {
                savedForegroundColor = ForegroundColor;
                ForegroundColor = Brushes.Gray;
            }
        }
    }
}
