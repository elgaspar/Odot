using Odot.Views.Assist;
using System.Collections.Generic;

namespace Odot.ViewModels
{
    class DialogSettingsViewModel : DialogViewModelBase
    {
        public DialogSettingsViewModel(MainViewModel parent) : base(parent)
        {
            loadCurrentSettings();
        }

        private bool _openLastUsedFile;
        public bool OpenLastUsedFile
        {
            get { return _openLastUsedFile; }
            set
            {
                _openLastUsedFile = value;
                NotifyOfPropertyChange(() => OpenLastUsedFile);
            }
        }

        private bool _showConfirmWhenRemoving;
        public bool ShowConfirmWhenRemoving
        {
            get { return _showConfirmWhenRemoving; }
            set
            {
                _showConfirmWhenRemoving = value;
                NotifyOfPropertyChange(() => ShowConfirmWhenRemoving);
            }
        }

        public List<string> AvailableThemes { get { return Settings.AvailableThemes; } }

        private string _selectedTheme;
        public string SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                _selectedTheme = value;
                NotifyOfPropertyChange(() => SelectedTheme);
            }
        }



        public void Ok()
        {
            saveCurrentSettings();
            TryClose(true);
        }

        public void Cancel()
        {
            //Do nothing
            TryClose(null);
        }



        private void loadCurrentSettings()
        {
            OpenLastUsedFile = Settings.OpenLastUsedFile;
            ShowConfirmWhenRemoving = Settings.ShowConfirmWhenRemoving;
            SelectedTheme = Settings.Theme;
        }

        private void saveCurrentSettings()
        {
            Settings.OpenLastUsedFile = OpenLastUsedFile;
            Settings.ShowConfirmWhenRemoving = ShowConfirmWhenRemoving;
            Settings.Theme = SelectedTheme;
        }
    }
}
