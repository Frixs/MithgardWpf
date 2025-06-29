// Author: Tomáš Frixs

using MithgardWpf.App.Core.ViewModels.Common;

namespace MithgardWpf.App.Core.ViewModels.Abstractions;

/// <summary>
///     Defines a simplified interface for retrieving view models from the DI container,
///     without requiring direct access to the full <see cref="IServiceProvider"/>.
/// </summary>
public interface IViewModelProvider
{
    /// <summary>
    ///     Retrieves view model from the service collection by using <see cref="IServiceProvider"/>.
    /// </summary>
    /// <typeparam name="T">The desired view model type to retrieve.</typeparam>
    /// <returns>The view model instance of type <typeparamref name="T"/> or <see langword="null"/> if not found.</returns>
    T? Get<T>()
        where T : IViewModel;

    /// <summary>
    ///     Retrieves view model from the service collection by using <see cref="IServiceProvider"/>.
    /// </summary>
    /// <typeparam name="T">The desired view model type to retrieve.</typeparam>
    /// <returns>The view model instance of type <typeparamref name="T"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the view model of type <typeparamref name="T"/> is not registered in the service collection.</exception>
    T GetRequired<T>()
        where T : IViewModel;
}
