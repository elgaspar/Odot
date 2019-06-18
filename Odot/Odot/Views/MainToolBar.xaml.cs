using Odot.Views.Assist;
using System.Windows;
using System.Windows.Controls;

namespace Odot.Views
{
    /// <summary>
    /// Interaction logic for ToolBar.xaml
    /// </summary>
    public partial class MainToolBar : UserControl
    {
        public MainToolBar()
        {
            InitializeComponent();
        }

        private void Button_Click_New(object sender, RoutedEventArgs e)
        {
            Actions.FileNew();
        }

        private void Button_Click_Open(object sender, RoutedEventArgs e)
        {
            Actions.FileOpen();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            Actions.FileSave();
        }

        private void Button_Click_SaveAs(object sender, RoutedEventArgs e)
        {
            Actions.FileSaveAs();
        }

        private void MenuItem_Click_PrintAll(object sender, RoutedEventArgs e)
        {
            Actions.PrintAll();
        }

        private void MenuItem_Click_PrintIncomplete(object sender, RoutedEventArgs e)
        {
            Actions.PrintIncomplete();
        }



        private void Button_Click_Settings(object sender, RoutedEventArgs e)
        {
            Actions.Settings();
        }

        private void Button_Click_About(object sender, RoutedEventArgs e)
        {
            Actions.About();
        }

        private void MenuItem_Click_PDF_ExportAll(object sender, RoutedEventArgs e)
        {
            Actions.PDFExportAll();
        }

        private void MenuItem_Click_PDF_ExportIncomplete(object sender, RoutedEventArgs e)
        {
            Actions.PDFExportIncomplete();
        }
    }
}
