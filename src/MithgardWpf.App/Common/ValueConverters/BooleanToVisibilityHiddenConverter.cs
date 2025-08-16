// Author: Tomáš Frixs

using MithgardWpf.App.Common.ValueConverters.Common;
using System.Globalization;
using System.Windows;

namespace MithgardWpf.App.Common.ValueConverters;

/// <summary>
///     Converter used for transforming boolean value to <see cref="Visibility.Visible"/> (<see langword="true"/>) or <see cref="Visibility.Hidden"/> (<see langword="false"/>).<para/>
///     When the converter parameter is defined (the value does not matter, e.g. <see langword="true"/>), it will invert the logic.
/// </summary>
public sealed class BooleanToVisibilityHiddenConverter : SingleValueConverter<BooleanToVisibilityHiddenConverter>
{
    /// <inheritdoc />
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is null)
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        else
            return (bool)value ? Visibility.Hidden : Visibility.Visible;
    }

    /// <inheritdoc />
    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
