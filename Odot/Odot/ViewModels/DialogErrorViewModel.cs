using Caliburn.Micro;

namespace Odot.ViewModels
{
    class DialogErrorViewModel : Screen
    {
        public DialogErrorViewModel(string msg)
        {
            Msg = msg;
        }

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

        public void Ok()
        {
            //Do nothing
            TryClose(true);
        }
    }
}
