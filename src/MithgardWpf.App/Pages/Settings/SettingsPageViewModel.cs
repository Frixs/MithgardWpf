// Author: Tomáš Frixs

using CommunityToolkit.Mvvm.Input;
using MithgardWpf.App.Core.Navigation;
using MithgardWpf.App.Core.Navigation.Abstractions;
using MithgardWpf.App.Core.ViewModels.Common;
using System.Windows.Input;

namespace MithgardWpf.App.Pages.Settings;

/// <summary>
///     View model for the <see cref="SettingsPageView"/> page.
/// </summary>
public class SettingsPageViewModel : ObservableViewModel, IPageViewModel
{
    /// <inheritdoc />
    public static string PageIdentifier => "Settings";

    /// <summary>
    ///     Reference to the navigation service used to navigate between pages in the application.
    /// </summary>
    private readonly INavigationService _navigation;

    // Dummy command to show how to use the view model (manual definition).
    public ICommand NavigateToHomeCommand { get; set; }

    // Dummy command to show how to use the view model (manual definition).
    public ICommand NavigateToProfileCommand { get; set; }

    /// <summary>
    ///     Default parameterless constructor every view model should have for design-time purposes.
    /// </summary>
    public SettingsPageViewModel()
    {
        _navigation = null!;

        NavigateToHomeCommand = new RelayCommand(NavigateToHome);
        NavigateToProfileCommand = new RelayCommand(NavigateToProfile);
    }

    /// <summary>
    ///     DI-specific consntructor.
    /// </summary>
    /// <param name="navigation">Navigation too be able to navigate between app's pages.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public SettingsPageViewModel(INavigationService navigation) : this()
    {
        _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
    }

    // Dummy command method to show how to use the view model (manual definition).
    public void NavigateToHome()
    {
        _navigation.NavigateTo(AppPageIdentifier.Home.ToString());
    }

    // Dummy command method to show how to use the view model (manual definition).
    public void NavigateToProfile()
    {
        _navigation.NavigateTo(AppPageIdentifier.Profile.ToString());
    }
}
