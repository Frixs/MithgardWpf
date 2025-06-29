// Author: Tomáš Frixs

using MithgardWpf.App.Core.Navigation.Abstractions;
using MithgardWpf.App.Core.Navigation.Views;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace MithgardWpf.App.Core.Navigation;

/// <summary>
///     Implements the mapping methods that helps resolve page identifiers to their respective view models and views.<para/>
///     It creates a map of the related types into the dictionary map at the initialization time.
/// </summary>
internal sealed class PageMapper : IPageMapper
{
    /// <summary>
    ///     Refernece to the logger.
    /// </summary>
    private readonly ILogger<PageMapper> _logger;

    /// <summary>
    ///     A dictionary that maps page keys to their associated ViewModel and View types.
    /// </summary>
    /// <remarks>This mapping is used to resolve the appropriate ViewModel and View for a given page identifier.</remarks>
    private readonly Dictionary<string, (Type ViewModelType, Type ViewType)> _pageMap;

    /// <summary>
    ///     Default constructor that initializes the page map by scanning assemblies for all pages.
    /// </summary>
    public PageMapper(ILogger<PageMapper> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _pageMap = BuildPageMap();
    }

    /// <inheritdoc/>
    public string GetIdentifier(AppPage page)
    {
        return _pageMap
            .FirstOrDefault(kvp => kvp.Value.ViewType == page.GetType())
            .Key;
    }

    /// <inheritdoc/>
    public AppPage? GetNewPageView(string pageIdentifier, object? pageViewModel = null)
    {
        if (pageIdentifier is null)
        {
            _logger.LogError("Page identifier cannot be null.");
            return null;
        }

        if (!_pageMap.TryGetValue(pageIdentifier, out var pageInfo))
        {
            _logger.LogError("Page identifier '{PageIdentifier}' not found in the page map.", pageIdentifier);
            return null;
        }

        if (pageViewModel is not null && !CheckViewModel(pageIdentifier, pageViewModel))
        {
            _logger.LogError(
                "Provided view model type '{ViewModelType}' does not match expected type '{ExpectedViewModelType}' for page identifier '{PageIdentifier}'.",
                pageViewModel.GetType(), 
                pageInfo.ViewModelType, 
                pageIdentifier
            );
            return null;
        }

        var instance = pageViewModel is null
            ? Activator.CreateInstance(pageInfo.ViewType)
            : Activator.CreateInstance(pageInfo.ViewType, pageViewModel);

        if (instance is not AppPage page)
        {
            _logger.LogCritical(
                "Failed to create an instance of type '{PageType}' as a page for page identifier '{PageIdentifier}'.",
                pageInfo.ViewType, 
                pageIdentifier
            );
            return null;
        }

        return page;
    }

    /// <inheritdoc/>
    public bool CheckViewModel(string pageIdentifier, object pageViewModel)
    {
        var pageInfo = _pageMap[pageIdentifier];

        if (!pageViewModel.GetType().Equals(pageInfo.ViewModelType))
            return false;

        return true;
    }

    /// <summary>
    ///     Builds a page map pairing page identifiers with their respective view model types and view types.
    ///     <para/>
    ///     It scans assemblies to find all view types that implement base page implementation (<see cref="AppPage{}"/>).
    ///     Each page then must specify a view model type bound to the view as the generic parameter of the base page type.
    ///     The view model type is constrained to <see cref="IPageViewModel"/>, which enables filtering of relevant types.
    ///     Each view model type must have an <see cref="IPageViewModel.PageIdentifier"/> property of type string that uniquely identifies the page.
    /// </summary>
    /// <returns>Populated map with all found identifiers as keys, where each valueis a tuple containing the respective view model type and view type.</returns>
    private Dictionary<string, (Type ViewModelType, Type ViewType)> BuildPageMap()
    {
        var result = new Dictionary<string, (Type ViewModelType, Type ViewType)>();

        // Define the types we are looking for.
        Type lookupPageType = typeof(AppPage<>);
        Type lookupViewModelType = typeof(IPageViewModel);

        // Scan all loaded assemblies for all existing types.
        var allTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes()); // get all types in the assembly

        // Search for the types we are looking for and populate the map.
        foreach (var type in allTypes)
        {
            // Filter out abstract types and all non-class types (e.g. interfaces)
            if (!type.IsClass || type.IsAbstract)
                continue;

            // Try to get the base type the type inherits from (if any exists).
            Type? baseType = type.BaseType;
            // Make sure the base type exists and it is a generic type.
            // If both conditions are met, check if it is the type we are looking for.
            if (baseType is null || !baseType.IsGenericType || baseType.GetGenericTypeDefinition() != lookupPageType)
                continue;

            // At this point, we can say with certainty that the currently inspected type inherits from the lookup page type.
            // Each such page defines a page specific view model in the base type generic parameter that is specifically paired with the page type.

            // Get the generic argument of the base type, which should be the page view model type.
            Type vmType = baseType.GetGenericArguments()[0]; // at this point, we're certain it's a generic type, meaning it has at least one type argument
            // Make sure the page view model type implements the interface we are looking for.
            if (!lookupViewModelType.IsAssignableFrom(vmType))
                continue; // as the page should allow only the page view models, this should not occur in the perfect world

            // Each page view model type should have a public static property of type string that uniquely identifies the page.
            // Try to get the looked-for public static property.
            PropertyInfo? identifierProp = vmType.GetProperty(nameof(IPageViewModel.PageIdentifier), BindingFlags.Public | BindingFlags.Static);
            // Make sure the property is not null and has a getter method.
            if (identifierProp?.GetMethod is null)
                continue; // as the interface forces to implement it, this should not occur in the perfect world
            // Try to extract the identifier value from the property.
            string identifierValue = (identifierProp.GetValue(null) as string)?.Trim()!;
            if (string.IsNullOrEmpty(identifierValue))
            {
                _logger.LogCritical(
                    "Page view model type '{ViewModelType}' does not have a valid value for page identifier property '{IdentifierProperty}'.",
                    vmType, 
                    nameof(IPageViewModel.PageIdentifier)
                );
                continue;
            }

            // Make sure there is no duplicity in the map.
            if (!result.ContainsKey(identifierValue))
            {
                result[identifierValue] = (vmType, type);
            }
            // Otherwise, duplicity is found ...
            else
            {
                _logger.LogCritical(
                    "Duplicate page identifier '{Identifier}' found for types '{ExistingType}' and '{NewType}'.",
                    identifierValue, 
                    result[identifierValue].ViewType, 
                    type
                );
            }
        }

        return result;
    }
}
