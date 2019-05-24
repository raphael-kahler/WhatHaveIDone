using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Whid.AppDetails;
using Whid.Framework.DB;

namespace Whid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            var dbName = "data.db";
            var dbPath = new AppFileHelper().GetApplicationFilePath(dbName);

            var service = new DbSummaryService(dbPath);

            DataContext = _viewModel = new MainWindowViewModel(service);
        }

        private void BringSelectionIntoView(object sender, SelectionChangedEventArgs e)
        {
            Selector selector = sender as Selector;
            if (selector is ListView listView)
            {
                listView.ScrollIntoView(selector.SelectedItem);
            }
        }
    }
}
