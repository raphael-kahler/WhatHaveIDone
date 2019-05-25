using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Whid.AppDetails;
using Whid.Framework;
using Whid.Framework.DB;
using Whid.Views.Main;

namespace Whid
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            new AppFileHelper().EnsureApplicationDirectoryExists();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddSingleton<ISummaryService>((provider) => new DbSummaryService(new AppFileHelper().GetApplicationFilePath("data.db")));
        }
    }
}
