// Author: Tomáš Frixs

using MithgardWpf.App.Common.ValueConvertors.Common;
using System.Globalization;
using System.Windows;

namespace MithgardWpf.App.Common.ValueConvertors;

/// <summary>
///     Converter used for transforming boolean value to <see cref="Visibility.Visible"/> (<see langword="true"/>) or <see cref="Visibility.Collapsed"/> (<see langword="false"/>).<para/>
///     When the converter parameter is defined (the value does not matter, e.g. <see langword="true"/>), it will invert the logic.
/// </summary>
public sealed class BooleanToVisibilityCollapsedConverter : SingleValueConverter<BooleanToVisibilityCollapsedConverter>
{
    /// <inheritdoc />
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (parameter is null)
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        else
            return (bool)value ? Visibility.Collapsed : Visibility.Visible;
    }

    /// <inheritdoc />
    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
