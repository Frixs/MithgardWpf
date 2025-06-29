// Author: Tomáš Frixs

using MithgardWpf.App.Core.ViewModels.Common;
using System.Windows;

namespace MithgardWpf.App.Core.ViewModels;

/// <summary>
///     Base view model logic for any <see cref="Window"/>.
/// </summary>
public class WindowViewModel : ObservableViewModel, IViewModel
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public WindowViewModel()
    {
    }
}
