using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using RegistrationSample.DesktopUI.Library.API;
using RegistrationSample.DesktopUI.Library.Models;
using RegistrationSample.DesktopUI.Library.Utilities;
using RegistrationSample.DesktopUI.ViewModels;
using RegistrationSample.DesktopUI.Views;
using RegistrationSample.OldDesktopUI.Library.Utilities;

namespace RegistrationSample.DesktopUI
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
        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton(sp => sp);
            services.AddSingleton<IApiHelper, ApiHelper>();
            services.AddSingleton<ILoggedInUserModel, LogedInUserModel>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IShellViewModel, ShellViewModel>();
            services.AddScoped<IViewModel, EntryViewModel>();
            services.AddTransient<IUserEndpoint, UserEndpoint>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<IViewModel, UserViewModel>();
            services.AddScoped<ShellView>();
            services.AddSingleton(s => GetShellWindow("Registration Sample", s));

            return services.BuildServiceProvider();
        }
        private static Window GetShellWindow(string title, IServiceProvider serviceProvider)
        {
            return new Window
            {
                Title = title,
                DataContext = serviceProvider.GetRequiredService<IShellViewModel>(),
                Content = serviceProvider.GetRequiredService<ShellView>(),
                MinHeight = 450,
                MinWidth = 450,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
        }
    }
}