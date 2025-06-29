// Author: Tomáš Frixs

using MithgardWpf.App.Core.Navigation;
using MithgardWpf.App.Core.Navigation.Abstractions;
using MithgardWpf.App.Core.ViewModels;
using MithgardWpf.App.Core.ViewModels.Abstractions;
using MithgardWpf.App.Pages.Home;
using MithgardWpf.App.Pages.Profile;
using MithgardWpf.App.Pages.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace MithgardWpf.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    /// <summary>
    ///     Gets the current <see cref="App"/> instance in use.
    /// </summary>
    public new static App Current => (App)Application.Current;

    /// <summary>
    ///     Gets the <see cref="IServiceProvider"/> instance to resolve application services.
    /// </summary>
    public IServiceProvider Services { get; }

    /// <summary>
    ///     Default constructor for the application.
    /// </summary>
    public App()
    {
        Services = ConfigureServices();

        InitializeComponent();
    }

    /// <summary>
    ///     Configures the services for the application.
    /// </summary>
    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Add logging
        services.AddLogging(logging =>
        {
            logging.ClearProviders();   // remove default console logger
            logging.AddDebug();         // log to debug output window
            logging.SetMinimumLevel(LogLevel.Trace);
        });
        
        // Windows
        services.AddSingleton(provider => new MainWindow()
        {
            DataContext = provider.GetRequiredService<WindowViewModel>()
        });

        // Services
        services.AddTransient<IViewModelProvider, ViewModelProvider>();
        // Services (Core/Navigation)
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IPageMapper, PageMapper>();

        // Viewmodels
        services.AddSingleton<AppViewModel>();
        services.AddTransient<WindowViewModel>();
        // Viewmodels (Pages)
        services.AddTransient<HomePageViewModel>();
        services.AddTransient<SettingsPageViewModel>();
        services.AddTransient<ProfilePageViewModel>();

        return services.BuildServiceProvider();
    }

    /// <inheritdoc/>
    protected override void OnStartup(StartupEventArgs e)
    {
        var logger = Services.GetRequiredService<ILogger<App>>();
        logger.LogInformation("Application starting up.");

        // Set up the main window and show it for the first time.
        var mainWindow = Services.GetRequiredService<MainWindow>();
        Current.MainWindow = mainWindow;
        mainWindow.Show();

        base.OnStartup(e);
    }

    #region Dispatcher Unhandled Exception

    private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(
            $"An unhandled exception just occurred: {e.Exception.Message}." +
            $"{Environment.NewLine}Please, contact the developers to fix the issue.", 
            "Fatal Error", 
            MessageBoxButton.OK, 
            MessageBoxImage.Warning
        );
        e.Handled = true;
    }

    #endregion
}
