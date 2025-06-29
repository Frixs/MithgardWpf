// Author: Tomáš Frixs

using CommunityToolkit.Mvvm.ComponentModel;

namespace MithgardWpf.App.Core.ViewModels.Common;

/// <summary>
///     The base view model that adds observable logic to the view model plus messaging logic as well.<para/>
///     This is the base class that view models inherit from.
/// </summary>
public abstract class RecipientViewModel : ObservableRecipient, IViewModel
{
}
