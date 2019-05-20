using System;
using System.Windows;
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

            DataContext = _viewModel = new MainWindowViewModel(new DbSummaryService(@"D:\temp\whid.db"));
        }

        private void ShowBiggerSummaries(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowBiggerSummaries();
        }

        private void ShowSmallerSummaries(object sender, RoutedEventArgs e)
        {
            _viewModel.ShowSmallerSummaries();
        }
    }
}
