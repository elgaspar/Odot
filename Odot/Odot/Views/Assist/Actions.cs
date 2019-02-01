using Odot.ViewModels;
using System.Windows;

namespace Odot.Views.Assist
{
    public static class Actions
    {
        private static MainViewModel mainVM { get { return (MainViewModel)Application.Current.MainWindow.DataContext; } }
        private static MainView mainWin { get { return (MainView)Application.Current.MainWindow; } }



        public static void FileNew()
        {
            bool succeed = promptForSave();
            if (succeed == true)
            {
                mainVM.New();
                updateStatusBar("New file created successfully.");
            }
        }

        public static void FileOpen()
        {
            bool succeed = promptForSave();
            if (succeed == true)
            {
                string filepath = Dialogs.BrowseFileToOpen();
                FileOpen(filepath);
            }
        }

        public static void FileOpen(string filepath)
        {
            if (filepath != null)
            {
                bool succeed = mainVM.Load(filepath);
                handleOpenResult(succeed);
            }
        }

        public static bool FileSave()
        {
            if (string.IsNullOrEmpty(mainVM.Filepath))
            {
                return FileSaveAs();
            }
            else
            {
                bool succeed = mainVM.Save(null);
                handleSaveResult(succeed);
                return succeed;
            }
        }

        public static bool FileSaveAs()
        {
            string filepath = Dialogs.BrowseFileToSave();
            if (filepath != null)
            {
                bool succeed = mainVM.Save(filepath);
                handleSaveResult(succeed);
                return succeed;
            }
            return true;
        }



        public static void PrintAll()
        {
            PrintDocument printDoc = new PrintDocument();
            Dialogs.PrintDialog(printDoc.Create(mainVM.File.Tasks));
        }

        public static void PrintIncomplete()
        {
            PrintDocument printDoc = new PrintDocument(true);
            Dialogs.PrintDialog(printDoc.CreateWithIncompleteOnly(mainVM.File.Tasks));
        }



        public static void Settings()
        {
            Dialogs.Settings(mainVM);
            Assist.Settings.ApplyTheme();
            updateStatusBar("Settings changed successfully.");
        }

        public static void About()
        {
            Dialogs.About();
        }



        public static void TaskAdd()
        {
            Dialogs.TaskAdd(mainVM);
            updateStatusBar("Task added successfully.");
        }

        public static void TaskEdit()
        {
            Dialogs.TaskEdit(mainVM);
            updateStatusBar("Task edited successfully.");
        }

        public static void TaskRemove()
        {
            Dialogs.Result result = Dialogs.Result.Yes;
            if (Assist.Settings.ShowConfirmWhenRemoving)
            {
                result = Dialogs.ConfirmTaskRemove();
                if (result == Dialogs.Result.Cancel)
                    return;
            }
            if (result == Dialogs.Result.Yes)
            {
                mainVM.TaskVM.Remove();
                updateStatusBar("Task removed successfully.");
            }
        }



        public static void CategoryAdd()
        {
            Dialogs.CategoryAdd(mainVM);
            updateStatusBar("Category Added successfully.");
        }

        public static void CategoryEdit()
        {
            Dialogs.CategoryEdit(mainVM);
            updateStatusBar("Category Edited successfully.");
        }

        public static void CategoryRemove()
        {
            Dialogs.Result result = Dialogs.Result.Yes;
            if (Assist.Settings.ShowConfirmWhenRemoving)
            {
                result = Dialogs.ConfirmCategoryRemove();
                if (result == Dialogs.Result.Cancel)
                    return;
            }
            if (result == Dialogs.Result.Yes)
            {
                mainVM.CategoryVM.Remove();
                updateStatusBar("Category removed successfully.");
            }
        }



        public static void Exit()
        {
            Assist.Settings.Save();

            bool succeed = promptForSave();
            if (succeed)
                Application.Current.Shutdown();
        }




        
        private static void updateStatusBar(string lastAction)
        {
            mainWin.statusBar.Update(lastAction);
        }



        private static void handleOpenResult(bool succeed)
        {
            string succeedMsg = "File opened successfully.";
            string failedMsg = "Couldn't open file.";
            handleResult(succeed, succeedMsg, failedMsg);
        }

        private static void handleSaveResult(bool succeed)
        {
            string succeedMsg = "File saved successfully.";
            string failedMsg = "Couldn't save file.";
            handleResult(succeed, succeedMsg, failedMsg);
        }

        private static void handleResult(bool succeed, string succeedMsg, string failedMsg)
        {
            if (succeed)
            {
                updateStatusBar(succeedMsg);
            }
            else
            {
                updateStatusBar(failedMsg);
                Dialogs.Error(failedMsg);
            }
        }



        private static bool promptForSave()
        {
            if (mainVM.AnyChangeHappened)
            {
                Dialogs.Result result = Dialogs.ConfirmPromptForSave();
                if (result == Dialogs.Result.Cancel)
                    return false;
                if (result == Dialogs.Result.Yes)
                {
                    bool saveSucceed = FileSave();
                    return saveSucceed;
                }
            }
            return true;
        }

    }
}
