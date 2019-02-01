using Caliburn.Micro;

namespace Odot.ViewModels
{
    public class DialogViewModelBase : Screen
    {
        public MainViewModel ParentVM { get; protected set; }

        public DialogViewModelBase(MainViewModel parent)
        {
            ParentVM = parent;
        }

        public DialogViewModelBase()
        {
        }
    }
}
