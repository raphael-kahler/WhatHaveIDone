using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Whid.Framework;
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

            var service = new DbSummaryService(@"D:\temp\whid.db");

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
