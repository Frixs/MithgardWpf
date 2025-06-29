// Author: Tomáš Frixs

using MithgardWpf.App.Core.Navigation.Views;

namespace MithgardWpf.App.Core.Navigation.Abstractions;

/// <summary>
///     Defines navigation operations for switching between <see cref="AppPage"/> views.
/// </summary>
public interface INavigationService
{
    /// <summary>
    ///     Identifier of the page being navigated to from the current page. 
    ///     <para/>
    ///     It serves the purposes of the navigation (e.g., checking the current page being displayed). 
    ///     It should not be used in other way.
    /// </summary>
    /// <remarks>
    ///     Commonly used in conjunction with <see cref="PageHost.CurrentPageIdentifierProperty"/> 
    ///     to listen for property change events.
    /// </remarks>
    string CurrentPageIdentifier { get; }

    /// <summary>
    ///     The explicitly defined view model to be used for the page being navigated to.<para/>
    ///     NOTE: This is not the current page’s up-to-date view model.
    ///     It is simply used to set the view model of the new current page at the time of navigation.
    ///     In other words, this property provides a tool to pass explicit view model data to the new page.
    /// </summary>
    /// <remarks>
    ///     If set, it overrides the default view model for the target page; otherwise, the default is used.
    ///     It is tightly coupled with <see cref="CurrentPageIdentifier"/>.
    /// </remarks>
    IPageViewModel? CurrentPageViewModel { get; }

    /// <summary>
    ///     Navigates to the specified page by the given page identifier.
    /// </summary>
    /// <param name="pageIdentifier">The page identifier specifying the target page to navigate to.</param>
    void NavigateTo(string pageIdentifier);

    /// <summary>
    ///     Navigates to the specified page by the given page identifier.
    /// </summary>
    /// <typeparam name="TViewModel">Type of the view model used for explicit initialization into the page.</typeparam>
    /// <param name="pageIdentifier">The page identifier specifying the target page to navigate to.</param>
    /// <param name="initViewModelAction">The action that can be used to modify the view model that is going to be explicitly put into the page.</param>
    /// <remarks>If the view model fails to initialize, the default view model associated with the page identifier will be used instead.</remarks>
    void NavigateTo<TViewModel>(string pageIdentifier, Action<TViewModel> initViewModelAction)
        where TViewModel : IPageViewModel;
}
