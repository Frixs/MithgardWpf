// Author: Tomáš Frixs

using MithgardWpf.App.Core.Navigation.Views;

namespace MithgardWpf.App.Core.Navigation.Abstractions;

/// <summary>
///     Defines helper methods based on the mapping between page identifiers and their corresponding view models and views.
/// </summary>
/// <remarks>The mapping is based on the <see cref="IPageViewModel.PageIdentifier"/> being defined in every page used for navigation purposes.</remarks>
public interface IPageMapper
{
    /// <summary>
    ///     Retrieves a page identifier for a given page.
    /// </summary>
    /// <param name="page">The given page.</param>
    /// <returns>The page identifier respective to the given page.</returns>
    string GetIdentifier(AppPage page);

    /// <summary>
    ///     Creates and returns a new page instance based on the specified page identifier.
    /// </summary>
    /// <param name="pageIdentifier">A unique string that identifies the type of page to create. This value cannot be null or empty.</param>
    /// <param name="pageViewModel">An optional view model to associate with the new page. If null, the page will be created with a default view model respective to the page type.</param>
    /// <returns>An instance of the page corresponding to the specified <paramref name="pageIdentifier"/>, or <see langword="null"/> if the page cannot be created.</returns>
    /// <remarks>
    ///     The method attempts to create a new page based on <paramref name="pageIdentifier"/>. If the identifier
    ///     does not match any known page type, the method returns <see langword="null"/>.<para/>
    ///     Also, the <paramref name="pageViewModel"/>, if given, must match the expected view model type for the page.
    /// </remarks>
    AppPage? GetNewPageView(string pageIdentifier, object? pageViewModel = null);

    /// <summary>
    ///     Checks if the provided view model matches the expected type for the specified page identifier.
    /// </summary>
    /// <param name="pageIdentifier">A unique string that identifies the type of page to create. This value cannot be null or empty.</param>
    /// <param name="pageViewModel">The view model instance to compare against the type mapped to the specified identifier.</param>
    /// <returns><see langword="true"/> if the type of the given <paramref name="pageViewModel"/> matches the view model type associated with <paramref name="pageIdentifier"/> in the map; otherwise, <see langword="false"/>.</returns>
    bool CheckViewModel(string pageIdentifier, object pageViewModel);
}
