using Odot.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Odot.Views
{
    /// <summary>
    /// Interaction logic for MyStatusBar.xaml
    /// </summary>
    public partial class MyStatusBar : StatusBar
    {
        private static MainViewModel mainVM { get { return (MainViewModel)Application.Current.MainWindow.DataContext; } }

        public MyStatusBar()
        {
            InitializeComponent();
        }



        public void Init()
        {
            Update(null);
        }

        public void Update(string lastAction)
        {
            const string space = "  ";
            StatusFileInfo.Text = space + getFileInfo();
            StatusLastAction.Text = lastAction + space;
        }



        private string getFileInfo()
        {
            return getFilepathString() + getTaskAndCategoriesCountString();
        }

        private string getFilepathString()
        {
            if (string.IsNullOrEmpty(mainVM.Filepath))
                return "Not saved.";
            return "File: " + mainVM.Filepath + "          ";
        }

        private string getTaskAndCategoriesCountString()
        {
            if (string.IsNullOrEmpty(mainVM.Filepath))
                return "";
            return getTaskCountString() + " and " + getCategoriesCountString();
        }

        private string getTaskCountString()
        {
            int taskCount = tasksCount(mainVM.File.Tasks);
            string str = "Total: " + taskCount + " task";
            if (taskCount != 1)
                str += "s";
            return str;
        }

        private string getCategoriesCountString()
        {
            int categoryCount = mainVM.CategoryVM.Categories.Count;
            string str = categoryCount + " categor";
            if (categoryCount != 1)
                str += "ies";
            else
                str += "y";
            str += ".";
            return str;
        }

        private int tasksCount(ObservableCollection<Models.Task> tasks)
        {
            int count = tasks.Count;
            foreach (Models.Task task in tasks)
            {
                count += tasksCount(task.Children);
            }
            return count;
        }
    }
}
