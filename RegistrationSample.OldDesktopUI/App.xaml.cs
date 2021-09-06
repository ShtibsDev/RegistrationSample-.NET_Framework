using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Library.Models;
using RegistrationSample.OldDesktopUI.Utility;
using RegistrationSample.OldDesktopUI.ViewModels;
using RegistrationSample.OldDesktopUI.Views;

namespace RegistrationSample.OldDesktopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _services;

        public App()
        {
            _services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var shellWindow = _services.GetService<Window>();
            shellWindow.Show();

            base.OnStartup(e);
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IServiceCollection, ServiceCollection>();
            services.AddSingleton<IApiHelper, ApiHelper>();
            services.AddSingleton<ILoggedInUserModel, LogedInUserModel>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<INavigation, Navigation>();
            services.AddTransient<ShellViewModel>();
            services.AddScoped<IViewModel, EntryViewModel>();
            services.AddScoped<IViewModel, LoginViewModel>();
            services.AddScoped<ShellView>();
            services.AddSingleton(s => GetShellWindow("Registration Sample", s));

            return services.BuildServiceProvider();
        }

        private static Window GetShellWindow(string title, IServiceProvider serviceProvider)
        {
            return new Window
            {
                Title = title,
                DataContext = serviceProvider.GetRequiredService<ShellViewModel>(),
                Content = serviceProvider.GetRequiredService<ShellView>(),
                MinHeight = 400,
                MinWidth = 400,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
        }
    }
}