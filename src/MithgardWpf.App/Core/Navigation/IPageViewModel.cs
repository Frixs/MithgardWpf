// Author: Tomáš Frixs

using MithgardWpf.App.Core.Navigation.Views;
using MithgardWpf.App.Core.ViewModels.Common;

namespace MithgardWpf.App.Core.Navigation;

/// <summary>
///     This interface represents a view model that is specifically designed for a page in the application.<para/>
///     The purpose of this interface is to provide a common type for all page view models, allowing other interfaces 
///     to constrain against it. All view models of this type are made to work with pages of type <see cref="AppPage"/>.
/// </summary>
public interface IPageViewModel : IViewModel
{
    /// <summary>
    ///     Defines the identifier that uniquely represents the page this view model is bound to.<para/>
    ///     Furthermore, it is used for navigation purposes.
    /// </summary>
    /// <remarks>It must not be empty or contain any whitespace.</remarks>
    public static abstract string PageIdentifier { get; }
}
