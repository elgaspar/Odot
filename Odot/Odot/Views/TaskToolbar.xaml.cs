using Odot.Views.Assist;
using System.Windows;
using System.Windows.Controls;

namespace Odot.Views
{
    /// <summary>
    /// Interaction logic for TaskToolbar.xaml
    /// </summary>
    public partial class TaskToolbar : ToolBarTray
    {
        public TaskToolbar()
        {
            InitializeComponent();
        }



        public bool ShowIncompleteTaskOnly
        {
            get { return (bool)GetValue(ShowIncompleteTaskOnlyProperty); }
            set { SetValue(ShowIncompleteTaskOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowIncompleteTaskOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowIncompleteTaskOnlyProperty =
            DependencyProperty.Register("ShowIncompleteTaskOnly", typeof(bool), typeof(TaskToolbar), new PropertyMetadata(false));



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
    }
}
