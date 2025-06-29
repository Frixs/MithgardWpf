// Author: Tomáš Frixs

using System.Windows;
using System.Windows.Media.Animation;

namespace MithgardWpf.App.Core.Animations;

/// <summary>
///     Extension methods adding animations to <see cref="FrameworkElement"/>s.
/// </summary>
public static class FrameworkElementExtensions
{
    /// <summary>
    ///     Slides the given element in the given direction and fades it in.
    /// </summary>
    /// <param name="element">The framework element to animate.</param>
    /// <param name="direction">The direction of the slide.</param>
    /// <param name="duration">
    ///     The time in seconds the whole animation will take.<para/>
    ///     Must be greater than 0, otherwise it is always 0.
    /// </param>
    /// <param name="distance">
    ///     Distance of slide animation as a value described in device-independent units (1/96th inch per unit).<para/>
    ///     Must be greater than 0, otherwise the actual width/height of the element is used.
    /// </param>
    /// <param name="skip">
    ///     Set to <see langword="true"/> to proceed to the expected state, but skip the entire 
    ///     animation (used when loading elements). Otherwise <see langword="false"/> (default).
    /// </param>
    /// <param name="preserveElementDimensions">Whether to keep the element at the same width/height during animation.</param>
    /// <returns>Task to be awaited.</returns>
    public static async Task SlideAndFadeInAsync(
        this FrameworkElement element,
        AnimationSlideDirection direction,
        float duration = 0.3f,
        int distance = 0,
        bool skip = false,
        bool preserveElementDimensions = true
        )
    {
        float durationVal = duration > 0 ? duration : 0;
        if (skip) durationVal = 0;

        var sb = new Storyboard();

        // Add the slide animation in the correct direction
        switch (direction)
        {
            case AnimationSlideDirection.Left:
                sb.AddSlideInFromLeft(
                    duration: durationVal,
                    offset: distance > 0 ? distance : element.ActualWidth, 
                    keepMargin: preserveElementDimensions
                    );
                break;

            case AnimationSlideDirection.Top:
                sb.AddSlideInFromTop(
                    duration: durationVal,
                    offset: distance > 0 ? distance : element.ActualHeight,
                    keepMargin: preserveElementDimensions
                    );
                break;

            case AnimationSlideDirection.Right:
                sb.AddSlideInFromRight(
                    duration: durationVal,
                    offset: distance > 0 ? distance : element.ActualWidth,
                    keepMargin: preserveElementDimensions
                    );
                break;

            case AnimationSlideDirection.Bottom:
                sb.AddSlideInFromBottom(
                    duration: durationVal,
                    offset: distance > 0 ? distance : element.ActualHeight,
                    keepMargin: preserveElementDimensions
                    );
                break;
        }
        // Add the fade in animation
        sb.AddFadeIn(durationVal);

        // Start the element animation
        sb.Begin(element);

        // Make sure, the element is set to visible when animating in
        element.Visibility = Visibility.Visible;

        // Wait for it to finish
        await Task.Delay((int)(durationVal * 1000));

        // Ensure the element remains visible at the end of the animation, even if it was interrupted.
        element.Visibility = Visibility.Visible;
    }

    /// <summary>
    ///     Slides the given element out the given direction and fades it out.
    /// </summary>
    /// <param name="element">The framework element to animate.</param>
    /// <param name="direction">The direction of the slide.</param>
    /// <param name="duration">
    ///     The time in seconds the whole animation will take.<para/>
    ///     Must be greater than 0, otherwise it is always 0.
    /// </param>
    /// <param name="distance">
    ///     Distance of slide animation as a value described in device-independent units (1/96th inch per unit).<para/>
    ///     Must be greater than 0, otherwise the actual width/height of the element is used.
    /// </param>
    /// <param name="skip">
    ///     Set to <see langword="true"/> to proceed to the expected state, but skip the entire 
    ///     animation (used when loading elements). Otherwise <see langword="false"/> (default).
    /// </param>
    /// <param name="collapse">
    ///     Set to <see langword="true"/> to apply <see cref="Visibility.Collapsed"/> at the end of the animation 
    ///     to hide it completely. Otherwise, <see cref="Visibility.Hidden"/> is applied (default).
    /// </param>
    /// <param name="preserveElementDimensions">Whether to keep the element at the same width/height during animation.</param>
    /// <returns>Task to be awaited.</returns>
    public static async Task SlideAndFadeOutAsync(
        this FrameworkElement element, 
        AnimationSlideDirection direction, 
        float duration = 0.3f, 
        int distance = 0,
        bool skip = false,
        bool collapse = false,
        bool preserveElementDimensions = true
        )
    {
        float durationVal = duration > 0 ? duration : 0;
        if (skip) durationVal = 0;

        var sb = new Storyboard();

        // Add the slide animation in the correct direction
        switch (direction)
        {
            case AnimationSlideDirection.Left:
                sb.AddSlideOutToLeft(
                    duration: durationVal,
                    offset: distance > 0 ? distance : element.ActualWidth,
                    keepMargin: preserveElementDimensions);
                break;

            case AnimationSlideDirection.Top:
                sb.AddSlideOutToTop(
                    duration: durationVal,
                    offset: distance > 0 ? distance : element.ActualHeight,
                    keepMargin: preserveElementDimensions);
                break;

            case AnimationSlideDirection.Right:
                sb.AddSlideOutToRight(
                    duration: durationVal,
                    offset: distance > 0 ? distance : element.ActualWidth,
                    keepMargin: preserveElementDimensions);
                break;

            case AnimationSlideDirection.Bottom:
                sb.AddSlideOutToBottom(
                    duration: durationVal,
                    offset: distance > 0 ? distance : element.ActualHeight,
                    keepMargin: preserveElementDimensions);
                break;
        }
        // Add the fade out animation
        sb.AddFadeOut(durationVal);

        // Start the element animation
        sb.Begin(element);
        if (durationVal != 0)
        {
            // Make sure, the element is set to visible when animating out
            element.Visibility = Visibility.Visible;

            // Wait for the animation to finish
            await Task.Delay((int)(durationVal * 1000));
        }

        // Do not display the element upon finishing the animation
        element.Visibility = collapse ? Visibility.Collapsed : Visibility.Hidden;
    }

    /// <summary>
    ///     Fades the given element in.
    /// </summary>
    /// <param name="element">The framework element to animate.</param>
    /// <param name="duration">
    ///     The time in seconds the whole animation will take.<para/>
    ///     Must be greater than 0, otherwise it is always 0.
    /// </param>
    /// <param name="skip">
    ///     Set to <see langword="true"/> to proceed to the expected state, but skip the entire 
    ///     animation (used when loading elements). Otherwise <see langword="false"/> (default).
    /// </param>
    /// <returns>Task to be awaited.</returns>
    public static async Task FadeInAsync(
        this FrameworkElement element, 
        float duration = 0.3f,
        bool skip = false
        )
    {
        float durationVal = duration > 0 ? duration : 0;
        if (skip) durationVal = 0;

        var sb = new Storyboard();

        // Add the fade in animation
        sb.AddFadeIn(durationVal);

        // Start the element animation
        sb.Begin(element);

        // Make sure, the element is set to visible when animating in
        element.Visibility = Visibility.Visible;

        // Wait for it to finish
        await Task.Delay((int)(durationVal * 1000));

        // Ensure the element remains visible at the end of the animation, even if it was interrupted.
        element.Visibility = Visibility.Visible;
    }

    /// <summary>
    ///     Fades the given element in without waiting -> possible animation interruption.
    /// </summary>
    /// <param name="element">The framework element to animate.</param>
    /// <param name="duration">
    ///     The time in seconds the whole animation will take.<para/>
    ///     Must be greater than 0, otherwise it is always 0.
    /// </param>
    /// <param name="skip">
    ///     Set to <see langword="true"/> to proceed to the expected state, but skip the entire 
    ///     animation (used when loading elements). Otherwise <see langword="false"/> (default).
    /// </param>
    public static void FadeInNoWait(
        this FrameworkElement element, 
        float duration = 0.3f,
        bool skip = false
        )
    {
        float durationVal = duration > 0 ? duration : 0;
        if (skip) durationVal = 0;

        var sb = new Storyboard();
        sb.Completed += (o, s) => { element.Visibility = Visibility.Visible; };

        // Add the fade in animation
        sb.AddFadeIn(durationVal);

        // Start the element animation
        sb.Begin(element);

        // Make sure, the element is set to visible when animating in
        element.Visibility = Visibility.Visible;
    }

    /// <summary>
    ///     Fades the given element out.
    /// </summary>
    /// <param name="element">The framework element to animate.</param>
    /// <param name="duration">
    ///     The time in seconds the whole animation will take.<para/>
    ///     Must be greater than 0, otherwise it is always 0.
    /// </param>
    /// <param name="skip">
    ///     Set to <see langword="true"/> to proceed to the expected state, but skip the entire 
    ///     animation (used when loading elements). Otherwise <see langword="false"/> (default).
    /// </param>
    /// <param name="collapse">
    ///     Set to <see langword="true"/> to apply <see cref="Visibility.Collapsed"/> at the end of the animation 
    ///     to hide it completely. Otherwise, <see cref="Visibility.Hidden"/> is applied (default).
    /// </param>
    /// <returns>Task to be awaited.</returns>
    public static async Task FadeOutAsync(
        this FrameworkElement element, 
        float duration = 0.3f,
        bool skip = false,
        bool collapse = false
        )
    {
        float durationVal = duration > 0 ? duration : 0;
        if (skip) durationVal = 0;

        var sb = new Storyboard();
        
        // Add the fade in animation
        sb.AddFadeOut(durationVal);

        // Start the element animation
        sb.Begin(element);
        if (durationVal != 0)
        {
            // Make sure, the element is set to visible when animating out
            element.Visibility = Visibility.Visible;

            // Wait for the animation to finish
            await Task.Delay((int)(durationVal * 1000));
        }

        // Do not display the element upon finishing the animation
        element.Visibility = collapse ? Visibility.Collapsed : Visibility.Hidden;
    }

    /// <summary>
    ///     Fades the given element out without waiting -> possible animation interruption.
    /// </summary>
    /// <param name="element">The framework element to animate.</param>
    /// <param name="duration">
    ///     The time in seconds the whole animation will take.<para/>
    ///     Must be greater than 0, otherwise it is always 0.
    /// </param>
    /// <param name="skip">
    ///     Set to <see langword="true"/> to proceed to the expected state, but skip the entire 
    ///     animation (used when loading elements). Otherwise <see langword="false"/> (default).
    /// </param>
    /// <param name="collapse">
    ///     Set to <see langword="true"/> to apply <see cref="Visibility.Collapsed"/> at the end of the animation 
    ///     to hide it completely. Otherwise, <see cref="Visibility.Hidden"/> is applied (default).
    /// </param>
    public static void FadeOutNoWait(
        this FrameworkElement element, 
        float duration = 0.3f, 
        bool skip = false,
        bool collapse = false
        )
    {
        float durationVal = duration > 0 ? duration : 0;
        if (skip) durationVal = 0;

        var sb = new Storyboard();
        sb.Completed += (o, s) => { element.Visibility = collapse ? Visibility.Collapsed : Visibility.Hidden; };

        // Add the fade in animation
        sb.AddFadeOut(durationVal);

        // Start the element animation
        sb.Begin(element);
        if (durationVal != 0)
            // Make sure, the element is set to visible when animating out
            element.Visibility = Visibility.Visible;
    }
}
