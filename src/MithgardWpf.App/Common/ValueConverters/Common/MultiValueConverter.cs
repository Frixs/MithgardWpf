// Author: Tomáš Frixs

using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MithgardWpf.App.Common.ValueConverters.Common;

/// <summary>
///     A base value converter that allows direct XAML usage for a multiple values.
/// </summary>
/// <typeparam name="T">The converter class type used.</typeparam>
public abstract class MultiValueConverter<T> : MarkupExtension, IMultiValueConverter
        where T : class, new()
{
    /// <summary>
    ///     A singleton static instance of this value converter.
    /// </summary>
    private static T _converter = null!; // see the constructor for initialization

    /// <summary>
    ///     Provides a static instance of the value converter.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>The convertor instance.</returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return _converter ??= new T();
    }

    /// <inheritdoc />
    public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

    /// <inheritdoc />
    public abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
}
