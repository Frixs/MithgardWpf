// Author: Tomáš Frixs

using Microsoft.Extensions.DependencyInjection;

namespace MithgardWpf.App.Core.ViewModels;

/// <summary>
///     Singleton utility class to simplify locating key view models within the XAML UI.
/// </summary>
/// <remarks>
///     This instance isn't registered in the DI container. It exists independently to make it 
///     easier to bind to directly in XAML UI by setting it as the binding source.<para/>
///     It should not be used for any other purpose than to locate view models in the XAML UI.
/// </remarks>
public sealed class ViewModelLocator
{
    #region Singleton

    /// <summary>
    ///     Singleton instance of this locator.
    /// </summary>
    public static ViewModelLocator Instance { get; } = new();

    #endregion

    /// <summary>
    ///     Shortcut property to access the service provider from the current application instance.
    /// </summary>
    private IServiceProvider Services => MithgardWpf.App.App.Current.Services;

    /// <summary>
    ///     Reference to the app view model.
    /// </summary>
    public AppViewModel App => Services.GetRequiredService<AppViewModel>();
}
