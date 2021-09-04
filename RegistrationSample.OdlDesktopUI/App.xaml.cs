using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RegistrationSample.OldDesktopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application 
    { 
     private readonly IHost _host;

    public App()
    {
        _host = CreateHostBuilder().Build();
    }

    public IServiceProvider ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        _host.StartAsync();

        var shellWindow = _host.Services.GetRequiredService<Window>();
        shellWindow.Show();

        base.OnStartup(e);
    }

    static IHostBuilder CreateHostBuilder(string[] args = null)
    {
        return Host.CreateDefaultBuilder(args)

            .ConfigureServices(services =>
            {
                    //services.AddSingleton<IApiHelper, ApiHelper>();
                    services.AddScoped<ShellViewModel>();
                services.AddScoped<LoginViewModel>();
                services.AddScoped<ShellView>();
                services.AddSingleton(s =>
                    GetShellWindow("Registration Sample", s.GetRequiredService<ShellViewModel>(), s.GetRequiredService<ShellView>()));
            });

    }

    private static Window GetShellWindow<T, V>(string title, T dataContext, V content)
    {
        return new Window
        {
            Title = title,
            DataContext = dataContext,
            Content = content,
            MinHeight = 400,
            MinWidth = 400,
            SizeToContent = SizeToContent.WidthAndHeight,
            WindowStartupLocation = WindowStartupLocation.CenterScreen
        };
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();

        base.OnExit(e);
    }

}
}
