// Author: Tomáš Frixs

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MithgardWpf.App.Core.Navigation;
using MithgardWpf.App.Core.Navigation.Abstractions;
using MithgardWpf.App.Core.ViewModels.Common;
using MithgardWpf.App.Pages.Profile;

namespace MithgardWpf.App.Pages.Home;

/// <summary>
///     View model for the <see cref="HomePageView"/> page.
/// </summary>
public partial class HomePageViewModel : ObservableViewModel, IPageViewModel
{
    /// <inheritdoc />
    public static string PageIdentifier => "Home";

    /// <summary>
    ///     Reference to the navigation service used to navigate between pages in the application.
    /// </summary>
    private readonly INavigationService _navigation;

    // Dummy property to show how to use the view model.
    [ObservableProperty]
    private string _title = "";

    // Dummy property to show how to use the view model.
    [ObservableProperty]
    private bool _isAnimating = false;

    /// <summary>
    ///     Default parameterless constructor every view model should have for design-time purposes.
    /// </summary>
    public HomePageViewModel()
    {
        _navigation = null!;
    }

    /// <summary>
    ///     DI-specific constructor.
    /// </summary>
    /// <param name="navigation">Navigation too be able to navigate between app's pages.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public HomePageViewModel(INavigationService navigation) : this()
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

    // Dummy command method to show how to use the view model.
    [RelayCommand]
    public void NavigateToProfile()
    {
        _navigation.NavigateTo<ProfilePageViewModel>(AppPageIdentifier.Profile.ToString(), (vm) => { vm.Name = "I'm coming from the Home page!"; });
    }

    // Dummy command method to show how to use the view model.
    [RelayCommand]
    public void Animate()
    {
        IsAnimating = !IsAnimating;
        Title = "XOXO";
    }
}
