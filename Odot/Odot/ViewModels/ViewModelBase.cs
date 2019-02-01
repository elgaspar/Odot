using Caliburn.Micro;

namespace Odot.ViewModels
{
    public class ViewModelBase : PropertyChangedBase
    {
        protected MainViewModel ParentVM;

        public ViewModelBase(MainViewModel parent)
        {
            ParentVM = parent;
        }
    }
}
