// Author: Tomáš Frixs

using CommunityToolkit.Mvvm.ComponentModel;

namespace MithgardWpf.App.Core.ViewModels.Common;

/// <summary>
///     The base view model that adds observable logic to the view model.<para/>
///     This is the base class that view models inherit from.
/// </summary>
public abstract class ObservableViewModel : ObservableObject, IViewModel
{
}
