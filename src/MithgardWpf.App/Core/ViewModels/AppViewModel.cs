// Author: Tomáš Frixs

using MithgardWpf.App.Core.Navigation.Abstractions;
using MithgardWpf.App.Core.ViewModels.Common;
using System.Reflection;
using System.Windows;

namespace MithgardWpf.App.Core.ViewModels;

/// <summary>
///     Special singleton view model defining the current state of the running application.
/// </summary>
public sealed class AppViewModel : ObservableViewModel, IViewModel
{
    /// <summary>
    ///     Reference to the navigation service that allows navigating between pages.
    /// </summary>
    public INavigationService Navigation { get; }

    /// <summary>
    ///     Application's title that users can see in the app UI.
    /// </summary>
    public string Title { get; private init; }

    /// <summary>
    ///     Application's current assembly version.
    /// </summary>
    public string Version { get; private init; }

    /// <summary>
    ///     Default constructor.
    /// </summary>
    /// <param name="navigation">The navigation service used to navigate between pages in the app.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public AppViewModel(INavigationService navigation)
    {
        Navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));

        Title = "MithgardWpf Template";

        Version = Assembly.GetEntryAssembly()?
            .GetName()
            .Version?
            .ToString() ?? "Unknown";
    }

    /// <summary>
    ///     Displays the application's main window, ensuring it is visible, focused, and brought to the front.
    /// </summary>
    /// <remarks>
    ///     If the main window is minimized, it will be restored to its normal state. If the main window
    ///     is not currently visible, it will be initialized and shown. The method also ensures 
    ///     the window is activated and focused, bringing it to the foreground.
    /// </remarks>
    public void ShowMainWindow()
    {
        var window = App.Current.MainWindow;

        // If the window is already visible, bring it to the front (if it is not)
        if (window.IsVisible)
        {
            if (window.WindowState == WindowState.Minimized)
            {
                window.WindowState = WindowState.Normal;
            }
        }
        // Otherwise, initialize and show the window
        else
        {
            window.Show();
        }

        // Bring the window to the front and focus it
        window.Activate();
        window.Focus();
    }
}
