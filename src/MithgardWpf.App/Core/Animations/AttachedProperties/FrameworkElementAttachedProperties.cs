// Author: Tomáš Frixs

using MithgardWpf.App.Core.Animations.AttachedProperties.Common;
using System.Windows;

namespace MithgardWpf.App.Core.Animations.AttachedProperties;

/// <summary>
///     Animates a framework element by sliding it in from the left when shown and sliding it out to the left when hidden.
///     This animation preserves the size of the element during the animation.
///     <para/>
///     Set the value to <see langword="true"/> to animate in, and <see langword="false"/> to animate out.
/// </summary>
public class AnimateSlideLeftProperty : AnimateAttachedProperty<AnimateSlideLeftProperty>
{
    /// <inheritdoc/>
    protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
    {
        // Animate in
        if (value) await element.SlideAndFadeInAsync(
            direction: AnimationSlideDirection.Left,
            duration: 0.3f,
            distance: 0,
            skip: firstLoad,
            preserveElementDimensions: true
            );
        // Animate out
        else await element.SlideAndFadeOutAsync(
            direction: AnimationSlideDirection.Left,
            duration: 0.3f,
            distance: 0,
            collapse: false,
            skip: firstLoad,
            preserveElementDimensions: true
            );
    }
}

/// <summary>
///     Animates a framework element by sliding it in from the left when shown and sliding it out to the left when hidden.
///     <para/>
///     Set the value to <see langword="true"/> to animate in, and <see langword="false"/> to animate out.
/// </summary>
public class AnimateSlideLeftFreeProperty : AnimateAttachedProperty<AnimateSlideLeftFreeProperty>
{
    /// <inheritdoc/>
    protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
    {
        // Animate in
        if (value) await element.SlideAndFadeInAsync(
            direction: AnimationSlideDirection.Left,
            duration: 0.3f,
            distance: 0,
            skip: firstLoad,
            preserveElementDimensions: false
            );
        // Animate out
        else await element.SlideAndFadeOutAsync(
            direction: AnimationSlideDirection.Left,
            duration: 0.3f,
            distance: 0,
            collapse: false,
            skip: firstLoad,
            preserveElementDimensions: false
            );
    }
}

/// <summary>
///     Animates a framework element by sliding it in from the top when shown and sliding it out to the top when hidden.
///     This animation preserves the size of the element during the animation.
///     <para/>
///     Set the value to <see langword="true"/> to animate in, and <see langword="false"/> to animate out.
/// </summary>
public class AnimateSlideTopProperty : AnimateAttachedProperty<AnimateSlideTopProperty>
{
    /// <inheritdoc/>
    protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
    {
        // Animate in
        if (value) await element.SlideAndFadeInAsync(
            direction: AnimationSlideDirection.Top,
            duration: 0.3f,
            distance: 0,
            skip: firstLoad,
            preserveElementDimensions: true
            );
        // Animate out
        else await element.SlideAndFadeOutAsync(
            direction: AnimationSlideDirection.Top,
            duration: 0.3f,
            distance: 0,
            collapse: false,
            skip: firstLoad,
            preserveElementDimensions: true
            );
    }
}

/// <summary>
///     Animates a framework element by sliding it in from the top when shown and sliding it out to the top when hidden.
///     <para/>
///     Set the value to <see langword="true"/> to animate in, and <see langword="false"/> to animate out.
/// </summary>
public class AnimateSlideTopFreeProperty : AnimateAttachedProperty<AnimateSlideTopFreeProperty>
{
    /// <inheritdoc/>
    protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
    {
        // Animate in
        if (value) await element.SlideAndFadeInAsync(
            direction: AnimationSlideDirection.Top,
            duration: 0.3f,
            distance: 0,
            skip: firstLoad,
            preserveElementDimensions: false
            );
        // Animate out
        else await element.SlideAndFadeOutAsync(
            direction: AnimationSlideDirection.Top,
            duration: 0.3f,
            distance: 0,
            collapse: false,
            skip: firstLoad,
            preserveElementDimensions: false
            );
    }
}

/// <summary>
///     Animates a framework element by sliding it in from the right when shown and sliding it out to the right when hidden.
///     This animation preserves the size of the element during the animation.
///     <para/>
///     Set the value to <see langword="true"/> to animate in, and <see langword="false"/> to animate out.
/// </summary>
public class AnimateSlideRightProperty : AnimateAttachedProperty<AnimateSlideRightProperty>
{
    /// <inheritdoc/>
    protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
    {
        // Animate in
        if (value) await element.SlideAndFadeInAsync(
            direction: AnimationSlideDirection.Right,
            duration: 0.3f,
            distance: 0,
            skip: firstLoad,
            preserveElementDimensions: true
            );
        // Animate out
        else await element.SlideAndFadeOutAsync(
            direction: AnimationSlideDirection.Right,
            duration: 0.3f,
            distance: 0,
            collapse: false,
            skip: firstLoad,
            preserveElementDimensions: true
            );
    }
}

/// <summary>
///     Animates a framework element by sliding it in from the right when shown and sliding it out to the right when hidden.
///     <para/>
///     Set the value to <see langword="true"/> to animate in, and <see langword="false"/> to animate out.
/// </summary>
public class AnimateSlideRightFreeProperty : AnimateAttachedProperty<AnimateSlideRightFreeProperty>
{
    /// <inheritdoc/>
    protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
    {
        // Animate in
        if (value) await element.SlideAndFadeInAsync(
            direction: AnimationSlideDirection.Right,
            duration: 0.3f,
            distance: 0,
            skip: firstLoad,
            preserveElementDimensions: false
            );
        // Animate out
        else await element.SlideAndFadeOutAsync(
            direction: AnimationSlideDirection.Right,
            duration: 0.3f,
            distance: 0,
            collapse: false,
            skip: firstLoad,
            preserveElementDimensions: false
            );
    }
}

/// <summary>
///     Animates a framework element by sliding it in from the bottom when shown and sliding it out to the bottom when hidden.
///     This animation preserves the size of the element during the animation.
///     <para/>
///     Set the value to <see langword="true"/> to animate in, and <see langword="false"/> to animate out.
/// </summary>
public class AnimateSlideBottomProperty : AnimateAttachedProperty<AnimateSlideBottomProperty>
{
    /// <inheritdoc/>
    protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
    {
        // Animate in
        if (value) await element.SlideAndFadeInAsync(
            direction: AnimationSlideDirection.Bottom,
            duration: 0.3f,
            distance: 0,
            skip: firstLoad,
            preserveElementDimensions: true
            );
        // Animate out
        else await element.SlideAndFadeOutAsync(
            direction: AnimationSlideDirection.Bottom,
            duration: 0.3f,
            distance: 0,
            collapse: false,
            skip: firstLoad,
            preserveElementDimensions: true
            );
    }
}

/// <summary>
///     Animates a framework element by sliding it in from the bottom when shown and sliding it out to the bottom when hidden.
///     <para/>
///     Set the value to <see langword="true"/> to animate in, and <see langword="false"/> to animate out.
/// </summary>
public class AnimateSlideBottomFreeProperty : AnimateAttachedProperty<AnimateSlideBottomFreeProperty>
{
    /// <inheritdoc/>
    protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
    {
        // Animate in
        if (value) await element.SlideAndFadeInAsync(
            direction: AnimationSlideDirection.Bottom,
            duration: 0.3f,
            distance: 0,
            skip: firstLoad,
            preserveElementDimensions: false
            );
        // Animate out
        else await element.SlideAndFadeOutAsync(
            direction: AnimationSlideDirection.Bottom,
            duration: 0.3f,
            distance: 0,
            collapse: false,
            skip: firstLoad,
            preserveElementDimensions: false
            );
    }
}

/// <summary>
///     Animates a framework element by fading it in on show and fading it out on hide.
///     <para/>
///     Set the value to <see langword="true"/> to animate in, and <see langword="false"/> to animate out.
/// </summary>
public class AnimateFadeProperty : AnimateAttachedProperty<AnimateFadeProperty>
{
    /// <inheritdoc/>
    protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
    {
        // Animate in
        if (value) await element.FadeInAsync(
            duration: 0.3f,
            skip: firstLoad
            );
        // Animate out
        else await element.FadeOutAsync(
            duration: 0.3f,
            skip: firstLoad,
            collapse: false
            );
    }
}

/// <summary>
///     Animates a framework element by fading it in on show and fading it out on hide.
///     The animation goes without waiting -> possible animation interruption.
///     <para/>
///     Set the value to <see langword="true"/> to animate in, and <see langword="false"/> to animate out.
/// </summary>
public class AnimateFadeNoWaitProperty : AnimateAttachedProperty<AnimateFadeNoWaitProperty>
{
    /// <inheritdoc/>
    protected override void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
    {
        // Animate in
        if (value) element.FadeInNoWait(
            duration: 0.3f,
            skip: firstLoad
            );
        // Animate out
        else element.FadeOutNoWait(
            duration: 0.3f,
            skip: firstLoad,
            collapse: false
            );
    }
}
