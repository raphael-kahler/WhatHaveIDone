using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Whid.Framework;
using Whid.Views.About;

namespace Whid.Views.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ISummaryService service)
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(service);
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

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                SwitchMaximizedState();
            }
            else
            {
                Application.Current.MainWindow.DragMove();
            }
        }

        private void MinimizeWindow(object sender, EventArgs e) =>
            Application.Current.MainWindow.WindowState = WindowState.Minimized;

        private void MaximizeWindow(object sender, EventArgs e) =>
            SwitchMaximizedState();

        private void SwitchMaximizedState()
        {
            var window = Application.Current.MainWindow;

            // change window style temporarily to prevent window from hiding behind task bar when maximized. Style changed back at method exit.
            window.WindowStyle = WindowStyle.SingleBorderWindow;

            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
                MaximizeWindowButton.Content = "🗖";
            }
            else if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
                MaximizeWindowButton.Content = "🗗";
            }

            window.WindowStyle = WindowStyle.None;
        }

        private void CloseWindow(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void SelectColorScheme(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri($@"Resources\ColorSchemes\Colors{menuItem.Tag}.xaml", UriKind.Relative)
                });
            }

        }
    }
}
