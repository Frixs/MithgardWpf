// Author: Tomáš Frixs

namespace MithgardWpf.App.Pages.Home;

/// <summary>
///     Design-time view model for the <see cref="HomePageViewModel"/>.
/// </summary>
public sealed class HomePageDesignModel : HomePageViewModel
{
    /// <summary>
    ///     Design singleton reference for the XAML UI.
    /// </summary>
    public static HomePageDesignModel Instance => new();

    public HomePageDesignModel()
    {
        Title = "Home Page Design Model";
    }
}
