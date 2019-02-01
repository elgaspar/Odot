using Caliburn.Micro;
using IPrint;
using Microsoft.Win32;
using Odot.ViewModels;
using System.Windows;
using System.Windows.Documents;

namespace Odot.Views.Assist
{
    class Dialogs
    {
        private static readonly string FILE_EXTENSION = ".xml";
        private static readonly string FILE_FILTER = "XML files (*.xml)|*.xml";
        private static readonly string CONFIRM_TASK_REMOVE_MSG = "This action will delete the selected\ntask and all its sub-tasks.\nAre you sure you want to continue?";
        private static readonly string CONFIRM_CATEGORY_REMOVE_MSG = "This action will delete the selected\ncategory. Are you sure you want to continue?";
        private static readonly string CONFIRM_PROMPT_FOR_SAVE_MSG = "Do you want to save changes to file?";



        public enum Result { Yes, No, Cancel}



        public static string BrowseFileToSave()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = FILE_EXTENSION;
            dialog.Filter = FILE_FILTER;
            bool? result = dialog.ShowDialog();
            if (result != true) //null or false
                return null;
            return dialog.FileName;
        }

        public static string BrowseFileToOpen()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = FILE_EXTENSION;
            dialog.Filter = FILE_FILTER;
            bool? result = dialog.ShowDialog();
            if (result != true) //null or false
                return null;
            return dialog.FileName;
        }



        public static void Settings(MainViewModel mainVM)
        {
            showDialog(new DialogSettingsViewModel(mainVM));
        }

        public static void About()
        {
            Dialogs.showDialog(new DialogAboutViewModel());
        }



        public static void TaskAdd(MainViewModel mainVM)
        {
            Dialogs.showDialog(new DialogTaskAddEditViewModel(mainVM, true));
        }

        public static void TaskEdit(MainViewModel mainVM)
        {
            Dialogs.showDialog(new DialogTaskAddEditViewModel(mainVM, false));
        }

        public static Result ConfirmTaskRemove()
        {
            return Dialogs.confirmDialog(CONFIRM_TASK_REMOVE_MSG);
        }



        public static void CategoryAdd(MainViewModel mainVM)
        {
            Dialogs.showDialog(new DialogCategoryAddEditViewModel(mainVM, true));
        }

        public static void CategoryEdit(MainViewModel mainVM)
        {
            Dialogs.showDialog(new DialogCategoryAddEditViewModel(mainVM, false));
        }

        public static Result ConfirmCategoryRemove()
        {
            return Dialogs.confirmDialog(CONFIRM_CATEGORY_REMOVE_MSG);
        }



        public static Result ConfirmPromptForSave()
        {
            return Dialogs.confirmDialog(CONFIRM_PROMPT_FOR_SAVE_MSG);
        }



        public static void PrintDialog(FlowDocument doc)
        {
            IPrintDialog.PreviewDocument(doc, "Odot", true, Application.Current.MainWindow);
        }

        
        public static void Error(string errorMsg)
        {
            DialogErrorViewModel vm = new DialogErrorViewModel(errorMsg);
            IWindowManager manager = new WindowManager();
            manager.ShowDialog(vm, null, null);
        }





        private static void showDialog(DialogViewModelBase vm)
        {
            IWindowManager manager = new WindowManager();
            manager.ShowDialog(vm, null, null);
        }

        private static Result confirmDialog(string msg)
        {
            DialogConfirmViewModel vm = new DialogConfirmViewModel(msg);
            showDialog(vm);
            return boolToYesNoCancel(vm.Result);
        }

        private static Result boolToYesNoCancel(bool? value)
        {
            if (value == true)
                return Result.Yes;
            if (value == false)
                return Result.No;
            return Result.Cancel;
        }
    }
}
