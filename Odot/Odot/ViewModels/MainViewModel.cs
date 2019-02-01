using Caliburn.Micro;
using Odot.Models;

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
