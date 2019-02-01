using Odot.Views.Assist;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Odot.Views
{
    /// <summary>
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class CategoryView : UserControl
    {
        public CategoryView()
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

        private void ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            clearSelection();
        }

        private void ListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clearSelection();
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem item = ItemsControl.ContainerFromElement(sender as ItemsControl, (DependencyObject)e.OriginalSource) as ListBoxItem;
            if (item != null)
                Actions.CategoryEdit();
        }

        private void clearSelection()
        {
            list.SelectedIndex = -1;
        }
    }
}
