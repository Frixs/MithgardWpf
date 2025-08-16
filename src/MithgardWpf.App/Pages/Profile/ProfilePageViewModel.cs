// Author: Tomáš Frixs

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MithgardWpf.App.Core.Navigation;
using MithgardWpf.App.Core.Navigation.Abstractions;
using MithgardWpf.App.Core.ViewModels.Common;

namespace MithgardWpf.App.Pages.Profile;

/// <summary>
///     View model for the <see cref="ProfilePageView"/> page.
/// </summary>
public partial class ProfilePageViewModel : ObservableViewModel, IPageViewModel
{
    /// <inheritdoc />
    public static string PageIdentifier => "Profile";

    /// <summary>
    ///     Reference to the navigation service used to navigate between pages in the application.
    /// </summary>
    private readonly INavigationService _navigation;

    // Dummy property to show how to use the view model.
    [ObservableProperty]
    private string _name = "Name Placeholder";

    /// <summary>
    ///     Default parameterless constructor every view model should have for design-time purposes.
    /// </summary>
    public ProfilePageViewModel()
    {
        _navigation = null!;
    }

    /// <summary>
    ///     DI-specific constructor.
    /// </summary>
    /// <param name="navigation">Navigation too be able to navigate between app's pages.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public ProfilePageViewModel(INavigationService navigation) : this()
    {
        _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
    }

    // Dummy command method to show how to use the view model.
    [RelayCommand]
    public void NavigateToHome()
    {
        _navigation.NavigateTo(AppPageIdentifier.Home.ToString());
    }

    // Dummy command method to show how to use the view model.
    [RelayCommand]
    public void NavigateToSettings()
    {
        _navigation.NavigateTo(AppPageIdentifier.Settings.ToString());
    }
}
