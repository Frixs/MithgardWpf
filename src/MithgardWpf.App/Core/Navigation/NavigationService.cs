// Author: Tomáš Frixs

using CommunityToolkit.Mvvm.ComponentModel;
using MithgardWpf.App.Core.Navigation.Abstractions;
using MithgardWpf.App.Core.ViewModels.Abstractions;
using MithgardWpf.App.Pages.Home;

namespace MithgardWpf.App.Core.Navigation;

/// <summary>
///     Navigation logic used to switch between pages in the application.
/// </summary>
internal sealed class NavigationService : ObservableObject, INavigationService
{
    /// <summary>
    ///     Reference to the service that can retrieve view models.
    /// </summary>
    private readonly IViewModelProvider _viewModelProvider;

    /// <inheritdoc />
    public string CurrentPageIdentifier { get; private set; } // on property changed must be set manually

    /// <inheritdoc />
    public IPageViewModel? CurrentPageViewModel { get; private set; } = null; // on property changed must be set manually

    /// <summary>
    ///     Default constructor.
    /// </summary>
    /// <param name="viewModelProvider">A provider to add the ability to retrieve page view models from the DI container as needed.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public NavigationService(IViewModelProvider viewModelProvider)
    {
        _viewModelProvider = viewModelProvider ?? throw new ArgumentNullException(nameof(viewModelProvider));

        // Default page to switch to when no page is specified.
        CurrentPageIdentifier = HomePageViewModel.PageIdentifier;
    }

    /// <inheritdoc />
    public void NavigateTo(string pageIdentifier)
    {
        // If the new page differs from the current page ...
        if (CurrentPageIdentifier != pageIdentifier)
        {
            // Switch page
            SwitchPage(pageIdentifier, null);
        }
    }

    /// <inheritdoc />
    public void NavigateTo<TViewModel>(string pageIdentifier, Action<TViewModel> initViewModelAction)
        where TViewModel : IPageViewModel
    {
        // If the new page differs from the current page ...
        if (CurrentPageIdentifier != pageIdentifier)
        {
            // Prepare the view model to be used explicitly for the new page.
            TViewModel pageViewModel = _viewModelProvider.GetRequired<TViewModel>();
            initViewModelAction.Invoke(pageViewModel);

            // Switch page
            SwitchPage(pageIdentifier, pageViewModel);
        }
    }

    /// <summary>
    ///     Initialize the page switch process.
    /// </summary>
    /// <param name="pageIdentifier">The page identifier specifying the target page to navigate to.</param>
    /// <param name="viewModel">The view model, if any, to set explicitly to the new page.</param>
    private void SwitchPage(string pageIdentifier, IPageViewModel? viewModel = null)
    {
        CurrentPageViewModel = viewModel;
        CurrentPageIdentifier = pageIdentifier;

        // Fire off the page on property changed event.
        OnPropertyChanged(nameof(CurrentPageViewModel));
        OnPropertyChanged(nameof(CurrentPageIdentifier));
    }
}
