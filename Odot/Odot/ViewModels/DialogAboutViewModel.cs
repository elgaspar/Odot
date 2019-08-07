using System.Diagnostics;

namespace Odot.ViewModels
{
    class DialogAboutViewModel : DialogViewModelBase
    {
        private const string VersionNo = "1.1.0";

        private const string MY_SITE_URL = @"http://www.elgaspar.com";
        private const string LICENSE_ODOT = @"Resources\LICENSE Odot.txt";

        private const string LICENSE_CALIBURN_MICRO = @"Resources\third-party licenses\LICENSE Caliburn.Micro.txt";
        private const string LICENSE_MAHAPPS_METRO = @"Resources\third-party licenses\LICENSE MahApps.Metro.txt";
        private const string LICENSE_CONTROLZ_EX = @"Resources\third-party licenses\LICENSE ControlzEx.txt";
        private const string LICENSE_GONG_WPF_DRAG_DROP = @"Resources\third-party licenses\LICENSE gong-wpf-dragdrop.txt";
        private const string LICENSE_IPRINT = @"Resources\third-party licenses\LICENSE IPrint.txt";
        private const string LICENSE_MATERIAL_DESIGN_GOOGLE_ICONS = @"Resources\third-party licenses\LICENSE Material Design Google Icons.txt";
        private const string LICENSE_MATERIAL_DESIGN_COMMUNITY_ICONS = @"Resources\third-party licenses\LICENSE Material Design Community Icons.txt";
        private const string LICENSE_MODERN_UI_ICONS = @"Resources\third-party licenses\LICENSE Modern UI Icons.txt";
        private const string LICENSE_PDF_SHARP = @"Resources\third-party licenses\LICENSE PDFsharp.txt";
        private const string LICENSE_MIGRA_DOC = @"Resources\third-party licenses\LICENSE PDFsharp-MigraDoc.txt";



        public string Version { get { return "version " + VersionNo; } }

        public void VisitMySite()
        {
            Process.Start(MY_SITE_URL);
        }

        public void ShowMyLicense()
        {
            Process.Start(LICENSE_ODOT);
        }

        public void ShowCaliburnMicroLicense()
        {
            Process.Start(LICENSE_CALIBURN_MICRO);
        }

        public void ShowMahAppsMetroLicense()
        {
            Process.Start(LICENSE_MAHAPPS_METRO);
        }

        public void ShowControlzExLicense()
        {
            Process.Start(LICENSE_CONTROLZ_EX);
        }

        public void ShowGongWpfDragdropLicense()
        {
            Process.Start(LICENSE_GONG_WPF_DRAG_DROP);
        }

        public void ShowIPrintLicense()
        {
            Process.Start(LICENSE_IPRINT);
        }

        public void ShowMaterialDesighGoogleIconsLicense()
        {
            Process.Start(LICENSE_MATERIAL_DESIGN_GOOGLE_ICONS);
        }

        public void ShowMaterialDesighCommunityIconsLicense()
        {
            Process.Start(LICENSE_MATERIAL_DESIGN_COMMUNITY_ICONS);
        }

        public void ShowModernUIIconsLicense()
        {
            Process.Start(LICENSE_MODERN_UI_ICONS);
        }

        public void ShowPDFsharpLicense()
        {
            Process.Start(LICENSE_PDF_SHARP);
        }

        public void ShowMigraDocLicense()
        {
            Process.Start(LICENSE_MIGRA_DOC);
        }

        public void Ok()
        {
            //Do nothing
            TryClose(true);
        }

    }
}
