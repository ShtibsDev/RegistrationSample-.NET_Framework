using System;
using System.Windows;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RegistrationSample.OldDesktopUI.Library.API;
using RegistrationSample.OldDesktopUI.Library.Models;
using RegistrationSample.OldDesktopUI.Models;
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
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LoggedInUserModel, UserDisplayModel>();
                cfg.CreateMap<UserDisplayModel, LoggedInUserModel>();
                cfg.CreateMap<NewUserDisplayModel, NewUserModel>();
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(sp => sp)
                .AddSingleton<IApiHelper, ApiHelper>()
                .AddSingleton<IEventAggregator, EventAggregator>()
                .AddSingleton(mapper)
                .AddSingleton<AuthenticatedUser>()
                .AddSingleton<UserDisplayModel>()
                .AddSingleton<ShellViewModel>()
                .AddSingleton<ShellView>()
                .AddScoped<EntryViewModel>()
                .AddTransient<IUserEndpoint, UserEndpoint>()
                .AddTransient<LoginViewModel>()
                .AddTransient<UserViewModel>()
                .AddTransient<RegistrationViewModel>()
                .AddSingleton(s => GetShellWindow("Registration Sample (Old)", s));

            return services.BuildServiceProvider();
        }
        private static Window GetShellWindow(string title, IServiceProvider serviceProvider)
        {
            return new Window
            {
                Title = title,
                DataContext = serviceProvider.GetRequiredService<ShellViewModel>(),
                Content = serviceProvider.GetRequiredService<ShellView>(),
                MinHeight = 450,
                MinWidth = 450,
                SizeToContent = SizeToContent.WidthAndHeight,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
        }
    }
}