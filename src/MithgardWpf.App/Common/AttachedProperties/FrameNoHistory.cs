// Author: Tomáš Frixs

using MithgardWpf.App.Common.AttachedProperties.Common;
using System.Windows;
using System.Windows.Controls;

namespace MithgardWpf.App.Common.AttachedProperties;

/// <summary>
///     This attached property is intended for a <see cref="Frame"/> to hide navigation pane and keep the navigation history empty.
/// </summary>
public class FrameNoHistory : ValueAttachedProperty<FrameNoHistory, bool>
{
    /// <inheritdoc/>
    public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        // Get the frame.
        Frame? frame = sender as Frame;
        if (frame is null)
            return;

        // Hide navigation bar.
        frame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;

        // Check if the frame already has the flag set to prevent multiple subscriptions.
        if (GetNoHistoryAttachedFlag(frame) is false)
        {
            SetNoHistoryAttachedFlag(frame, true); // set the flag

            // Clear history on navigate.
            frame.Navigated += (ss, ee) => ((Frame)ss).NavigationService.RemoveBackEntry();
        }
    }

    #region Helper Attached Property Flag

    private static readonly DependencyProperty NoHistoryAttachedFlagProperty =
    DependencyProperty.RegisterAttached(
        "NoHistoryAttachedFlag",
        typeof(bool),
        typeof(FrameNoHistory),
        new PropertyMetadata(false)
    );

    private static bool GetNoHistoryAttachedFlag(DependencyObject obj) =>
        (bool)obj.GetValue(NoHistoryAttachedFlagProperty);

    private static void SetNoHistoryAttachedFlag(DependencyObject obj, bool value) =>
        obj.SetValue(NoHistoryAttachedFlagProperty, value);

    #endregion
}

