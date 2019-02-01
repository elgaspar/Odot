using Caliburn.Micro;

namespace Odot.ViewModels
{
    class DialogConfirmViewModel : DialogViewModelBase
    {
        public DialogConfirmViewModel(string msg)
        {
            Msg = msg;
            Result = null;
        }

        public bool? Result;

        private string _msg;
        public string Msg
        {
            get { return _msg; }
            set
            {
                _msg = value;
                NotifyOfPropertyChange(() => Msg);
            }
        }

        public void Yes()
        {
            //do nothing
            Result = true;
            TryClose(true);
        }

        public void No()
        {
            //do nothing
            Result = false;
            TryClose(false);
        }

        public void Exit()
        {
            //do nothing
            Result = null;
            TryClose(null);
        }
    }
}
