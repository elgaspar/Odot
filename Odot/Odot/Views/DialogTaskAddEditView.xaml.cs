using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Media;

namespace Odot.Views
{
    /// <summary>
    /// Interaction logic for DialogTaskAddEditView.xaml
    /// </summary>
    public partial class DialogTaskAddEditView : MetroWindow
    {
        public DialogTaskAddEditView()
        {
            InitializeComponent();
        }

        private void ComboBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            iconCategories.ForegroundColor = Brushes.Gray;
        }
    }
}
