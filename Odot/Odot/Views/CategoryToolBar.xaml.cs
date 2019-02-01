using Odot.Views.Assist;
using System.Windows;
using System.Windows.Controls;

namespace Odot.Views
{
    /// <summary>
    /// Interaction logic for CategoryToolBar.xaml
    /// </summary>
    public partial class CategoryToolBar : ToolBarTray
    {
        public CategoryToolBar()
        {
            InitializeComponent();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Actions.CategoryAdd();
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Actions.CategoryEdit();
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            Actions.CategoryRemove();
        }
    }
}
