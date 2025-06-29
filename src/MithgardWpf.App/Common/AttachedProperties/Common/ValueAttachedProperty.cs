// Author: Tomáš Frixs

using System.Windows;

namespace MithgardWpf.App.Common.AttachedProperties.Common;

/// <summary>
///     A base attached property to introduce generic for generating custom 
///     attached properties for WPF UI elemnts as needed.
///     <para/>
///     Extends from this to define a custom attached property that can be then 
///     introduced as a parameter in a WPF UI element in XAML.
///     <para/>
///     To fully use the base of this attached property, you can utilizy either events or override methods
///     to define procedure happening after each change and update.
/// </summary>
/// <typeparam name="TElement">The class representing the UI element the attached property is made for.</typeparam>
/// <typeparam name="TProperty">The data type of the attached property.</typeparam>
/// <remarks>
///     In this class, you define a base logic on which you can extend and create a new class that defines 
///     the attached property you want. This allows the property to be used across different elements. 
///     On the other hand, regular dependency properties are defined directly in the code-behind of the control 
///     or element that owns them and are intended for use on that specific control.
///     <para/>
///     Attached properties are a special type of dependency property that allow you to add custom properties 
///     to any existing WPF elements without modifying their definitions. They're typically used to attach additional 
///     behavior or metadata to elements from the outside. When an attached property is set or triggered 
///     (e.g., through property change callbacks), you can define custom logic to respond to it.
///     <para/>
///     In contrast, regular dependency properties follow the same foundational principles but are intended 
///     to be used directly on the object that defines them. The key difference lies in how they’re accessed:
///     <list type="bullet">
///         <item>
///             Attached properties are defined with static GetX() and SetX() methods, 
///             allowing them to be set on any DependencyObject.
///         </item>
///         <item>
///             Regular dependency properties are exposed through standard CLR property wrappers (get/set) 
///             and are meant to be used on instances of the declaring type.
///         </item>
///     </list>
///     In short, attached properties are about extending other types, while regular dependency 
///     properties are about enriching your own type.
/// </remarks>
public abstract class ValueAttachedProperty<TElement, TProperty>
    where TElement : new()
{
    /// <summary>
    ///     An event you can hook into to perform actions when the value changes.
    /// </summary>
    public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };

    /// <summary>
    ///     An event you can hook into to perform actions when the value changes, even when the value is the same.
    /// </summary>
    public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

    #region Attached Property Definition

    /// <summary>
    ///     Singleton instance of our parent class.
    /// </summary>
    public static TElement Instance { get; private set; } = new TElement();

    /// <summary>
    ///     The attached property definition.
    /// </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
        "Value",
        typeof(TProperty),
        typeof(ValueAttachedProperty<TElement, TProperty>),
        new UIPropertyMetadata(
            default(TProperty),
            new PropertyChangedCallback(OnValuePropertyChanged),
            new CoerceValueCallback(OnValuePropertyUpdated)
            )
        );

    /// <summary>
    ///     Gets the attached property.
    /// </summary>
    /// <param name="d">The element to get the property from.</param>
    /// <returns></returns>
    public static TProperty GetValue(DependencyObject d) => (TProperty)d.GetValue(ValueProperty);

    /// <summary>
    ///     Sets the attached property.
    /// </summary>
    /// <param name="d">The element to get the property from.</param>
    /// <param name="value">The value to set the property to.</param>
    public static void SetValue(DependencyObject d, TProperty value) => d.SetValue(ValueProperty, value);

    /// <summary>
    /// The callback event when the <see cref="ValueProperty"/> is changed.
    /// </summary>
    /// <param name="d">The UI element that got changes in its attached property of this type.</param>
    /// <param name="e">The arguments for the event.</param>
    private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Call the parent function
        (Instance as ValueAttachedProperty<TElement, TProperty>)?.OnValueChanged(d, e);

        // Call event listeners
        (Instance as ValueAttachedProperty<TElement, TProperty>)?.ValueChanged(d, e);
    }

    /// <summary>
    ///     The callback event when the <see cref="ValueProperty"/> is changed, even if it is the same value.
    /// </summary>
    /// <param name="d">The UI element that got changes in its attached property of this type.</param>
    /// <param name="value">The new value that got updated.</param>
    /// <returns>The <paramref name="value"/> for chaining.</returns>
    private static object OnValuePropertyUpdated(DependencyObject d, object value)
    {
        // Call the parent function
        (Instance as ValueAttachedProperty<TElement, TProperty>)?.OnValueUpdated(d, value);

        // Call event listeners
        (Instance as ValueAttachedProperty<TElement, TProperty>)?.ValueUpdated(d, value);

        return value;
    }

    #endregion

    /// <summary>
    ///     The method you can override in a custom attached property class 
    ///     to fire actions when the attached property of is changed.
    /// </summary>
    /// <param name="sender">The UI element that got changes in its attached property of this type.</param>
    /// <param name="e">The arguments for this event.</param>
    public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }

    /// <summary>
    ///     The method you can override in a custom attached property class 
    ///     to fire actions when the attached property of is changed, even if the value is the same.
    /// </summary>
    /// <param name="sender">The UI element that got changes in its attached property of this type.</param>
    /// <param name="value">The new value that got updated.</param>
    public virtual void OnValueUpdated(DependencyObject sender, object value) { }
}

