// Author: Tomáš Frixs

using MithgardWpf.App.Core.ViewModels.Abstractions;
using MithgardWpf.App.Core.ViewModels.Common;
using Microsoft.Extensions.DependencyInjection;

namespace MithgardWpf.App.Core.ViewModels;

/// <summary>
///     Direct implementation of the view model provider interface.
/// </summary>
internal sealed class ViewModelProvider : IViewModelProvider
{
    /// <summary>
    ///     The service provider reference used in this object to access and retrieve view models.
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     Function shortcut to retrieve view models from the service provider.
    /// </summary>
    private Func<Type, IViewModel?> _viewModelFactory =>
        viewModelType => (IViewModel?)_serviceProvider.GetService(viewModelType);

    /// <summary>
    ///     Function shortcut to retrieve view models from the service provider, throwing an exception if not found.
    /// </summary>
    private Func<Type, IViewModel> _viewModelRequiredFactory =>
        viewModelType => (IViewModel)_serviceProvider.GetRequiredService(viewModelType);

    /// <summary>
    ///     Default constructor for the view model provider.
    /// </summary>
    /// <param name="serviceProvider"> the service provider used in this object to access and retrieve view models.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public ViewModelProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    /// <inheritdoc />
    public T? Get<T>()
        where T : IViewModel 
        => (T?)_viewModelFactory.Invoke(typeof(T));

    /// <inheritdoc />
    public T GetRequired<T>()
        where T : IViewModel 
        => (T)_viewModelRequiredFactory.Invoke(typeof(T));
}
