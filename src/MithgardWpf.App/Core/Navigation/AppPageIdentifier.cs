// Author: Tomáš Frixs

namespace MithgardWpf.App.Core.Navigation;

/// <summary>
///     Enumeration representing the identifiers for the pages in the application.<para/>
///     It serves as a way to uniquely identify each page for navigation purposes.
/// </summary>
/// <remarks>
///     Using this file is optional but recommended, as it eliminates the need to use strings directly for navigation.
///     The enum values correspond to <see cref="IPageViewModel.PageIdentifier"/> values for pages within the application.
///     <para/>
///     In simpler scenarios, this enum might seem unnecessary, as <see cref="IPageViewModel.PageIdentifier"/> 
///     can be accessed directly as a static property from the appropriate page's view model.
///     However, to maintain the architecture tidy and avoid using strings directly for navigation, 
///     it is recommended to use this enum. Doing so will not violate dependency restrictions.
///     <para/>
///     If this enum is used, its values must match the <see cref="IPageViewModel.PageIdentifier"/> 
///     property values of the page view models for correct functionality.
/// </remarks>
public enum AppPageIdentifier
{
    Home = 0,

    Settings = 1,

    Profile = 2,
}
