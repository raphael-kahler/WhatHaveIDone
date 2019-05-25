using System.Windows;
using System.Windows.Navigation;
using Whid.Helpers;

namespace Whid.Views.About
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void NavigateToHyperlink(object sender, RequestNavigateEventArgs e) =>
            _ = ProcessStarter.OpenInBrowser(e.Uri.AbsoluteUri);
    }
}
