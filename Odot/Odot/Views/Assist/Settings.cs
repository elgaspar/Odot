using MahApps.Metro;
using Odot.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Odot.Views.Assist
{
    public static class Settings
    {
        private static MainViewModel mainVM { get { return (MainViewModel)Application.Current.MainWindow.DataContext; } }



        public static bool OpenLastUsedFile
        {
            get { return Properties.Settings.Default.OpenLastUsedFile; }
            set
            {
                Properties.Settings.Default.OpenLastUsedFile = value;
                Properties.Settings.Default.Save();
            }
        }

        public static bool ShowConfirmWhenRemoving
        {
            get { return Properties.Settings.Default.ShowConfirmWhenRemoving; }
            set
            {
                Properties.Settings.Default.ShowConfirmWhenRemoving = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string Theme
        {
            get { return Properties.Settings.Default.Theme; }
            set
            {
                Properties.Settings.Default.Theme = value;
                Properties.Settings.Default.Save();
            }
        }

        public static string LastUsedFilepath
        {
            get { return Properties.Settings.Default.LastUsedFilepath; }
            set
            {
                Properties.Settings.Default.LastUsedFilepath = value;
                Properties.Settings.Default.Save();
            }
        }



        public static List<string> AvailableThemes
        {
            get
            {
                return new List<string>(){"Blue", "Cobalt", "Mauve", "Olive", "Sienna"};
            }
        }



        public static void Apply()
        {
            ApplyTheme();
            openLastUsedFileIfNeeded();
        }

        public static void Save()
        {
            LastUsedFilepath = mainVM.Filepath;
        }

        public static void ApplyTheme()
        {
            try
            {
                Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);
                ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(Theme), ThemeManager.GetAppTheme("BaseLight"));
            }
            catch (Exception)
            {
                Dialogs.Error("Couldn't apply theme.");
            }
        }



        private static void openLastUsedFileIfNeeded()
        {
            bool openFile = OpenLastUsedFile;
            bool validFile = !string.IsNullOrEmpty(LastUsedFilepath);
            if (openFile && validFile)
                mainVM.Load(LastUsedFilepath);
        }
    }
}
