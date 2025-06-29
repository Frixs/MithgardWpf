// Author: Tomáš Frixs

using MithgardWpf.App.Common.ValueConvertors.Common;
using System.Collections;
using System.Globalization;

namespace MithgardWpf.App.Common.ValueConvertors;

/// <summary>
///     Converter to transfer a collection into a bool based on having any items in it.<para/>
///     Has value <see langword="true"/> if the collection has any items, otherwise <see langword="false"/>.
/// </summary>
public sealed class CollectionHasAnyItemsConverter : SingleValueConverter<CollectionHasAnyItemsConverter>
{
    /// <inheritdoc />
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((ICollection)value).Count > 0;
    }

    /// <inheritdoc />
    public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
