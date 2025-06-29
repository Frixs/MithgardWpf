// Author: Tomáš Frixs

using System.Windows;
using System.Windows.Media.Animation;

namespace MithgardWpf.App.Core.Animations;

/// <summary>
///     Animation extensions for the <see cref="Storyboard"/>.
/// </summary>
public static class StoryboardExtensions
{
    #region LEFT Slide Animations

    /// <summary>
    ///     Adds a slide in animation from the left to the given <paramref name="storyboard"/>.
    ///     This is the actual animation happening on an element via <see cref="Storyboard"/>.
    /// </summary>
    /// <param name="storyboard">The instance to apply the animation to.</param>
    /// <param name="duration">The time in seconds the whole animation will take.</param>
    /// <param name="offset">Distance of slide animation as a value described in device-independent units (1/96th inch per unit).</param>
    /// <param name="decelerationRatio">The rate of deceleration.</param>
    /// <param name="keepMargin">Whether to keep the element at the same width/height during animation by using margins.</param>
    public static void AddSlideInFromLeft(this Storyboard storyboard, float duration, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
    {
        // Create the margin animate from right 
        var animation = new ThicknessAnimation
        {
            Duration = new Duration(TimeSpan.FromSeconds(duration)),
            From = new Thickness(-offset, 0, keepMargin ? offset : 0, 0),
            To = new Thickness(0),
            DecelerationRatio = decelerationRatio
        };

        // Set the target property name
        Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

        // Add this to the storyboard
        storyboard.Children.Add(animation);
    }

    /// <summary>
    ///     Adds a slide out animation to the left to the given <paramref name="storyboard"/>.
    ///     This is the actual animation happening on an element via <see cref="Storyboard"/>.
    /// </summary>
    /// <param name="storyboard">The instance to apply the animation to.</param>
    /// <param name="duration">The time in seconds the whole animation will take.</param>
    /// <param name="offset">Distance of slide animation as a value described in device-independent units (1/96th inch per unit).</param>
    /// <param name="decelerationRatio">The rate of deceleration.</param>
    /// <param name="keepMargin">Whether to keep the element at the same width/height during animation by using margins.</param>
    public static void AddSlideOutToLeft(this Storyboard storyboard, float duration, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
    {
        // Create the margin animate from right 
        var animation = new ThicknessAnimation
        {
            Duration = new Duration(TimeSpan.FromSeconds(duration)),
            From = new Thickness(0),
            To = new Thickness(-offset, 0, keepMargin ? offset : 0, 0),
            DecelerationRatio = decelerationRatio
        };

        // Set the target property name
        Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

        // Add this to the storyboard
        storyboard.Children.Add(animation);
    }

    #endregion

    #region TOP Slide Animations

    /// <summary>
    ///     Adds a slide in animation from the top to the given <paramref name="storyboard"/>.
    ///     This is the actual animation happening on an element via <see cref="Storyboard"/>.
    /// </summary>
    /// <param name="storyboard">The instance to apply the animation to.</param>
    /// <param name="duration">The time in seconds the whole animation will take.</param>
    /// <param name="offset">Distance of slide animation as a value described in device-independent units (1/96th inch per unit).</param>
    /// <param name="decelerationRatio">The rate of deceleration.</param>
    /// <param name="keepMargin">Whether to keep the element at the same width/height during animation by using margins.</param>
    public static void AddSlideInFromTop(this Storyboard storyboard, float duration, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
    {
        // Create the margin animate from right 
        var animation = new ThicknessAnimation
        {
            Duration = new Duration(TimeSpan.FromSeconds(duration)),
            From = new Thickness(0, -offset, 0, keepMargin ? offset : 0),
            To = new Thickness(0),
            DecelerationRatio = decelerationRatio
        };

        // Set the target property name
        Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

        // Add this to the storyboard
        storyboard.Children.Add(animation);
    }

    /// <summary>
    ///     Adds a slide out animation to the top to the given <paramref name="storyboard"/>.
    ///     This is the actual animation happening on an element via <see cref="Storyboard"/>.
    /// </summary>
    /// <param name="storyboard">The instance to apply the animation to.</param>
    /// <param name="duration">The time in seconds the whole animation will take.</param>
    /// <param name="offset">Distance of slide animation as a value described in device-independent units (1/96th inch per unit).</param>
    /// <param name="decelerationRatio">The rate of deceleration.</param>
    /// <param name="keepMargin">Whether to keep the element at the same width/height during animation by using margins.</param>
    public static void AddSlideOutToTop(this Storyboard storyboard, float duration, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
    {
        // Create the margin animate from right 
        var animation = new ThicknessAnimation
        {
            Duration = new Duration(TimeSpan.FromSeconds(duration)),
            From = new Thickness(0),
            To = new Thickness(0, -offset, 0, keepMargin ? offset : 0),
            DecelerationRatio = decelerationRatio
        };

        // Set the target property name
        Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

        // Add this to the storyboard
        storyboard.Children.Add(animation);
    }

    #endregion

    #region RIGHT Slide Animations

    /// <summary>
    ///     Adds a slide in animation from the right to the given <paramref name="storyboard"/>.
    ///     This is the actual animation happening on an element via <see cref="Storyboard"/>.
    /// </summary>
    /// <param name="storyboard">The instance to apply the animation to.</param>
    /// <param name="duration">The time in seconds the whole animation will take.</param>
    /// <param name="offset">Distance of slide animation as a value described in device-independent units (1/96th inch per unit).</param>
    /// <param name="decelerationRatio">The rate of deceleration.</param>
    /// <param name="keepMargin">Whether to keep the element at the same width/height during animation by using margins.</param>
    public static void AddSlideInFromRight(this Storyboard storyboard, float duration, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
    {
        // Create the margin animate from right 
        var animation = new ThicknessAnimation
        {
            Duration = new Duration(TimeSpan.FromSeconds(duration)),
            From = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
            To = new Thickness(0),
            DecelerationRatio = decelerationRatio
        };

        // Set the target property name
        Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

        // Add this to the storyboard
        storyboard.Children.Add(animation);
    }

    /// <summary>
    ///     Adds a slide out animation to the right to the given <paramref name="storyboard"/>.
    ///     This is the actual animation happening on an element via <see cref="Storyboard"/>.
    /// </summary>
    /// <param name="storyboard">The instance to apply the animation to.</param>
    /// <param name="duration">The time in seconds the whole animation will take.</param>
    /// <param name="offset">Distance of slide animation as a value described in device-independent units (1/96th inch per unit).</param>
    /// <param name="decelerationRatio">The rate of deceleration.</param>
    /// <param name="keepMargin">Whether to keep the element at the same width/height during animation by using margins.</param>
    public static void AddSlideOutToRight(this Storyboard storyboard, float duration, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
    {
        // Create the margin animate from right 
        var animation = new ThicknessAnimation
        {
            Duration = new Duration(TimeSpan.FromSeconds(duration)),
            From = new Thickness(0),
            To = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
            DecelerationRatio = decelerationRatio
        };

        // Set the target property name
        Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

        // Add this to the storyboard
        storyboard.Children.Add(animation);
    }

    #endregion

    #region BOTTOM Slide Animations

    /// <summary>
    ///     Adds a slide in animation from the bottom to the given <paramref name="storyboard"/>.
    ///     This is the actual animation happening on an element via <see cref="Storyboard"/>.
    /// </summary>
    /// <param name="storyboard">The instance to apply the animation to.</param>
    /// <param name="duration">The time in seconds the whole animation will take.</param>
    /// <param name="offset">Distance of slide animation as a value described in device-independent units (1/96th inch per unit).</param>
    /// <param name="decelerationRatio">The rate of deceleration.</param>
    /// <param name="keepMargin">Whether to keep the element at the same width/height during animation by using margins.</param>
    public static void AddSlideInFromBottom(this Storyboard storyboard, float duration, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
    {
        // Create the margin animate from right 
        var animation = new ThicknessAnimation
        {
            Duration = new Duration(TimeSpan.FromSeconds(duration)),
            From = new Thickness(0, keepMargin ? offset : 0, 0, -offset),
            To = new Thickness(0),
            DecelerationRatio = decelerationRatio
        };

        // Set the target property name
        Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

        // Add this to the storyboard
        storyboard.Children.Add(animation);
    }

    /// <summary>
    ///     Adds a slide out animation to the bottom to the given <paramref name="storyboard"/>.
    ///     This is the actual animation happening on an element via <see cref="Storyboard"/>.
    /// </summary>
    /// <param name="storyboard">The instance to apply the animation to.</param>
    /// <param name="duration">The time in seconds the whole animation will take.</param>
    /// <param name="offset">Distance of slide animation as a value described in device-independent units (1/96th inch per unit).</param>
    /// <param name="decelerationRatio">The rate of deceleration.</param>
    /// <param name="keepMargin">Whether to keep the element at the same width/height during animation by using margins.</param>
    public static void AddSlideOutToBottom(this Storyboard storyboard, float duration, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
    {
        // Create the margin animate from right 
        var animation = new ThicknessAnimation
        {
            Duration = new Duration(TimeSpan.FromSeconds(duration)),
            From = new Thickness(0),
            To = new Thickness(0, keepMargin ? offset : 0, 0, -offset),
            DecelerationRatio = decelerationRatio
        };

        // Set the target property name
        Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

        // Add this to the storyboard
        storyboard.Children.Add(animation);
    }

    #endregion

    #region Fade Animations

    /// <summary>
    ///     Adds a fade in animation to the given <paramref name="storyboard"/>.
    ///     This is the actual animation happening on an element via <see cref="Storyboard"/>.
    /// </summary>
    /// <param name="storyboard">The instance to apply the animation to.</param>
    /// <param name="duration">The time in seconds the whole animation will take.</param>
    /// <param name="from">The level of visibility from where to start animating. The default value is set to 0 (completely hidden), and it always animates up to 1 (fully visible).</param>
    public static void AddFadeIn(this Storyboard storyboard, float duration, float from = 0f)
    {
        // Create the margin animate from right 
        var animation = new DoubleAnimation
        {
            Duration = new Duration(TimeSpan.FromSeconds(duration)),
            From = from is < 0 or > 1 ? 0 : from,
            To = 1,
        };

        // Set the target property name
        Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

        // Add this to the storyboard
        storyboard.Children.Add(animation);
    }

    /// <summary>
    ///     Adds a fade out animation to the given <paramref name="storyboard"/>.
    ///     This is the actual animation happening on an element via <see cref="Storyboard"/>.
    /// </summary>
    /// <param name="storyboard">The instance to apply the animation to.</param>
    /// <param name="duration">The time in seconds the whole animation will take.</param>
    /// <param name="from">The level of visibility from where to start animating. The default value is set to 1 (fully visible), and it always animates up to 0 (completely hidden).</param>
    public static void AddFadeOut(this Storyboard storyboard, float duration, float from = 1f)
    {
        // Create the margin animate from right 
        var animation = new DoubleAnimation
        {
            Duration = new Duration(TimeSpan.FromSeconds(duration)),
            From = from is < 0 or > 1 ? 1 : from,
            To = 0,
        };

        // Set the target property name
        Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));

        // Add this to the storyboard
        storyboard.Children.Add(animation);
    }

    #endregion
}
