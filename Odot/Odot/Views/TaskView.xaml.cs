using Odot.ViewModels;
using Odot.Views.Assist;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Odot.Views
{
    /// <summary>
    /// Interaction logic for Tasks.xaml
    /// </summary>
    public partial class TaskView : UserControl
    {
        public TaskView()
        {
            InitializeComponent();
        }



        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Actions.TaskAdd();
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            Actions.TaskEdit();
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            Actions.TaskRemove();
        }

        private void ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            clearSelection();
        }



        private void tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Models.Task selectedTask = e.NewValue as Models.Task;
            TaskViewModel vm = (TaskViewModel)DataContext;
            vm.SelectedTask = selectedTask;
        }

        private void tree_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                TreeViewItem treeViewItem = findSelected(e.OriginalSource as DependencyObject);
                if (treeViewItem != null)
                    treeViewItem.IsSelected = true;
                else
                    clearSelection();
            }
            else
                clearSelection();
        }

        private void clearSelection()
        {
            TreeViewItem item = tree.ItemContainerGenerator.ContainerFromIndex(0) as TreeViewItem;
            if (item != null)
            {
                item.IsSelected = true;
                item.IsSelected = false;
            }
        }

        static TreeViewItem findSelected(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);
            return source as TreeViewItem;
        }

        private void tree_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                e.Handled = true;
                return;
            }
            TreeViewItem item = ItemsControl.ContainerFromElement(sender as ItemsControl, (DependencyObject)e.OriginalSource) as TreeViewItem;
            if (item != null)
                Actions.TaskEdit();
            e.Handled = true;
        }

    }
}
