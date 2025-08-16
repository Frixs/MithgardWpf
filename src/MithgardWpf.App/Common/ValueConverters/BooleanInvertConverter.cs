// Author: Tomáš Frixs

using MithgardWpf.App.Common.ValueConverters.Common;
using System.Globalization;

namespace MithgardWpf.App.Common.ValueConverters;

/// <summary>
///     Converter used for inverting boolean value.
/// </summary>
public sealed class BooleanInvertConverter : SingleValueConverter<BooleanInvertConverter>
{
    /// <inheritdoc />
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !(bool)value;
    }

    /// <inheritdoc />
    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
