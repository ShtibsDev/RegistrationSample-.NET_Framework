using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Library.Models;
using RegistrationSample.OldDesktopUI.Library.Utilities;
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

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton(sp => sp);
            services.AddSingleton<IApiHelper, ApiHelper>();
            services.AddSingleton<IUserEndpoint, UserEndpoint>();
            services.AddSingleton<ILoggedInUserModel, LogedInUserModel>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddSingleton<IShellViewModel, ShellViewModel>();
            services.AddScoped<IViewModel, EntryViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<IViewModel, UserViewModel>();
            services.AddScoped<ShellView>();
            services.AddSingleton(s => GetShellWindow("Registration Sample", s));

            return services.BuildServiceProvider();
        }
        //private void ConfigureNavigationService()
        //{
        //    var viewModels = _services.GetServices<IViewModel>();
        //    var eventAggregator = _services.GetService<IEventAggregator>();
        //    var navigation = new Navigation(viewModels, eventAggregator);




        //}
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