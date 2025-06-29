// Author: Tomáš Frixs

using MithgardWpf.App.Common.AttachedProperties.Common;
using System.Windows;

namespace MithgardWpf.App.Core.Animations.AttachedProperties.Common;

/// <summary>
///     Represents an base attached property that provides animation behavior 
///     for a <see cref="FrameworkElement"/> when value gets updated.
/// </summary>
/// <typeparam name="TElement">The type of the attached property.</typeparam>
/// <remarks>
///     This class manages animations triggered by changes to the attached property's value.
///     Subclasses should override <see cref="DoAnimation(FrameworkElement, bool, bool)"/> to define 
///     the specific animation behavior.
/// </remarks>
public abstract class AnimateAttachedProperty<TElement> : ValueAttachedProperty<TElement, bool>
    where TElement : ValueAttachedProperty<TElement, bool>, new()
{
    /// <summary>
    ///     Information about framework elements that have already been loaded at least once.
    ///     They get tracked by a weak reference to know if they have been loaded before or not.
    ///     <para/>
    ///     The value pair consists of the weak reference to the element and a boolean. The boolean
    ///     value indicates whether it already finished its first load (<see langword="true"/>) or 
    ///     the element is in the middle of the first animation (<see cref="false"/>).
    /// </summary>
    protected Dictionary<WeakReference, bool> AlreadyLoaded { get; set; } = [];

    /// <summary>
    ///     The most recent value used for the first load.
    ///     This tracking makes sure the correct value gets used 
    ///     even when the value changes during or before the actual first load.
    ///     <para/>
    ///     The value pair consists of the weak reference to the element and a boolean. The boolean
    ///     value indicates the value that should be used for the first load animation.
    ///     <para/>
    ///     The first load animation defines the element’s initial state. It's not the actual animation itself, 
    ///     but the starting point from which the element can begin animating after the initial load.
    /// </summary>
    protected Dictionary<WeakReference, bool> FirstLoadValue { get; set; } = [];

    /// <summary>
    ///     On every value update, this method gets called to handle the initialization logic of the animation.
    /// </summary>
    /// <param name="sender">The UI element that got changes in its attached property of this type.</param>
    /// <param name="value">The new value that got updated.</param>
    public override void OnValueUpdated(DependencyObject sender, object value)
    {
        // Get the framework element
        if (sender is not FrameworkElement element)
            return;

        // Try and get the already loaded reference
        var alreadyLoadedReference = AlreadyLoaded.FirstOrDefault(f => f.Key.Target == sender);
        // Try and get the first load reference
        var firstLoadReference = FirstLoadValue.FirstOrDefault(f => f.Key.Target == sender);

        // Don't fire if the value doesn't change
        if ((bool)sender.GetValue(ValueProperty) == (bool)value && alreadyLoadedReference.Key != null)
            return;

        // On the first load ...
        if (alreadyLoadedReference.Key is null)
        {
            // Save the reference to the element
            var weakReference = new WeakReference(sender);
            // Mark it for any future updates so we know it was processed at least once.
            AlreadyLoaded[weakReference] = false;

            // Set it to start off hidden before we decide how to animate.
            element.Visibility = Visibility.Hidden;

            // Create a single self-unhookable event callback for the element.
            // This is needed to ensure that the animation only fires once after the element is fully loaded and ready.
            RoutedEventHandler? onLoaded = null;
            // This callback is intended to be fired once the framework element is fully loaded and ready.
            onLoaded = async (ss, ee) =>
            {
                // Unhook ourselves at the first place!!!
                element.Loaded -= onLoaded;

                // Do a slight delay after load that is needed for some elements
                // to get laid out and their width/heights correctly calculated.
                await Task.Delay(5);

                // Refresh the first load value in case it changed after the delay occurred.
                firstLoadReference = FirstLoadValue.FirstOrDefault(f => f.Key.Target == sender);

                // Do the desired animation.
                DoAnimation(
                    element: element,
                    value: firstLoadReference.Key != null ? firstLoadReference.Value : (bool)value, // use a more up-to-date value if available
                    firstLoad: true
                    );

                // Flag that we have finished first load.
                AlreadyLoaded[weakReference] = true;
            };

            // Hook into the Loaded event of the element
            // Once the element gets loaded, the callback gets fired in which the animation is done.
            element.Loaded += onLoaded;
        }
        // Fallback to catch the proper value for the first load when it started, but the animation was not finished yet ...
        else if (alreadyLoadedReference.Value == false)
        {
            FirstLoadValue[new WeakReference(sender)] = (bool)value;
        }
        // Any other animation requested on value update ...
        else
        {
            // Do desired animation
            DoAnimation(element, (bool)value, firstLoad: false);
        }
    }

    /// <summary>
    ///     The animation method that is fired when the value changes.
    /// </summary>
    /// <param name="element">The element to do the animation for.</param>
    /// <param name="value">The new value</param>
    /// <param name="firstLoad">
    ///     The first load animation defines the element’s initial state. It's not the actual animation itself, 
    ///     but the starting point from which the element can begin animating after the initial load.
    ///     <para/>
    ///     Set to <see langword="true"/> to move it to such state. 
    ///     Otherwise, set to <see langword="false"/> to leave a normal animation.
    /// </param>
    protected virtual void DoAnimation(FrameworkElement element, bool value, bool firstLoad) { }
}
