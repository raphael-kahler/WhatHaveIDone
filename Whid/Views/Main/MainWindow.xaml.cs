using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Whid.Framework;
using Whid.Views.About;

namespace Whid.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow(ISummaryService service)
        {
            InitializeComponent();
            DataContext = _viewModel = new MainWindowViewModel(service);
        }

        private void QuitApplication(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BringSelectionIntoView(object sender, SelectionChangedEventArgs e)
        {
            Selector selector = sender as Selector;
            if (selector is ListView listView)
            {
                listView.ScrollIntoView(selector.SelectedItem);
            }
        }

        private void ShowAboutPage(object sender, EventArgs e)
        {
            var aboutPage = new AboutWindow();
            aboutPage.Show();
        }
    }
}
