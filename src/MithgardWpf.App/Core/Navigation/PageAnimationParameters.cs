// Author: Tomáš Frixs

namespace MithgardWpf.App.Core.Navigation;

/// <summary>
///     Parameters wrapper for page-specific animations.
/// </summary>
/// <param name="LoadAnimation">The type of animation used for loading the whole page when it is selected to show.</param>
/// <param name="UnloadAnimation">The type of animation used for unloading the whole page.</param>
/// <param name="Duration">The time any slide animation takes to complete in seconds.</param>
/// <param name="SlideDistance">Distance of slide animation as a value described in device-independent units (1/96th inch per unit).</param>
public sealed record PageAnimationParameters(
    PageAnimationStyle LoadAnimation, 
    PageAnimationStyle UnloadAnimation, 
    float Duration,
    int SlideDistance
    );
