using Caliburn.Micro;
using Odot.Models;
using Odot.Views.Assist;
using System;
using System.Windows.Input;

namespace Odot.ViewModels
{
    public class MainViewModel : PropertyChangedBase
    {
        public TaskViewModel TaskVM { get; }
        public CategoryViewModel CategoryVM { get; }

        public TaskFile File { get; set; }

        public string Filepath { get; private set; }

        public bool AnyChangeHappened { get; set; }

        public MainViewModel()
        {
            File = new TaskFile();
            Filepath = null;

            TaskVM = new TaskViewModel(this);
            CategoryVM = new CategoryViewModel(this);

            AnyChangeHappened = false;

        }

        private ICommand PrintAllCommand { get; } = new RelayCommand((a) => Actions.PrintAll());
        public ICommand PrintCommand { get; } = new RelayCommand((a) => Actions.PrintIncomplete());
        public ICommand SaveCommand { get; } = new RelayCommand((a) => Actions.FileSave());
        public ICommand SaveAsCommand { get; } = new RelayCommand((a) => Actions.FileSaveAs());
        public ICommand NewCommand { get; } = new RelayCommand((a) => Actions.FileNew());
        public ICommand ExportIncompleteCommand { get; } = new RelayCommand(async (a) => await Actions.PDFExportIncomplete());
        public ICommand ExportAllCommand { get; } = new RelayCommand(async (a) => await Actions.PDFExportAll());
        public ICommand OpenCommand { get; } = new RelayCommand((a) => Actions.FileOpen());
        public ICommand AddTaskCommand { get; } = new RelayCommand((a) => Actions.TaskAdd());
        public ICommand AddCategoryCommand { get; } = new RelayCommand((a) => Actions.CategoryAdd());


        public void New()
        {
            File.Clear();
            Filepath = null;
            AnyChangeHappened = false;
            NotifyAll();
        }

        public bool Save(string path)
        {
            string pathToSave;
            if ( string.IsNullOrEmpty(path) )
                pathToSave = Filepath;
            else
                pathToSave = path;
            bool succeed = File.Save(pathToSave);
            if (succeed)
            {
                Filepath = pathToSave;
                AnyChangeHappened = false;
            }
            return succeed;
        }
        
        public bool Load(string path)
        {
            bool succeed = File.Load(path);
            if (succeed)
            {
                Filepath = path;
                AnyChangeHappened = false;
            }
            NotifyAll();
            return succeed;
        }

        public void NotifyAll()
        {
            TaskVM.NotifyAll();
            CategoryVM.NotifyAll();
        }
    }
}
