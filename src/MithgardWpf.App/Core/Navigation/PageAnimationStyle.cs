// Author: Tomáš Frixs

namespace MithgardWpf.App.Core.Navigation;

/// <summary>
///     Enumeration of page type animation styles available to choose from.
/// </summary>
public enum PageAnimationStyle
{
    /// <summary>
    ///     No animation (default).
    /// </summary>
    None = 0,

    /// <summary>
    ///     The page fades in and slides in from the bottom.
    /// </summary>
    SlideAndFadeInFromBottom = 1,

    /// <summary>
    ///     The page fades out and slides out to the bottom.
    /// </summary>
    SlideAndFadeOutToBottom = 2,
}
