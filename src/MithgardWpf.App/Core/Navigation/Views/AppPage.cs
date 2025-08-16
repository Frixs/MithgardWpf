// Author: Tomáš Frixs

using MithgardWpf.App.Core.Animations;
using MithgardWpf.App.Core.ViewModels.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MithgardWpf.App.Core.Navigation.Views;

/// <summary>
///     A base page for all pages to gain base navigation functionality.
/// </summary>
/// <remarks>This class defines common functionality for all application pages and is not intended to be directly inherited.</remarks>
public abstract class AppPage : Page
{
    /// <summary>
    ///     The view model associated with this page.
    /// </summary>
    private object? _boundViewModel;

    /// <summary>
    ///     The view model associated with this page.
    /// </summary>
    public object? ViewModelObject
    {
        get => _boundViewModel;
        set
        {
            // If they tend to be the same, do nothing.
            if (_boundViewModel == value)
                return;

            // Save the new view model.
            _boundViewModel = value;

            // Update the data context of this page.
            DataContext = _boundViewModel;
        }
    }

    /// <summary>
    ///     Indicates whether the page should animate when loading and unloading.
    /// </summary>
    public bool UseLoadUnloadAnimation { get; protected set; } = false;

    /// <summary>
    ///     Defines parameters needed for page animations when loading and unloading.
    ///     This heavily relies on the <see cref="UseLoadUnloadAnimation"/> flag.
    /// </summary>
    public PageAnimationParameters LoadUnloadAnimationParams { get; protected set; }

    /// <summary>
    ///     A flag that indicates whether to run the unload animation the next time 
    ///     this <see cref='Page'/> triggers its <see cref='FrameworkElement.Loaded'/> event.
    ///     <para/>
    ///     Keep this set to <see langword="false"/> as the default value to let 
    ///     the page loading normally. However, set this to <see langword='true'/> to trigger 
    ///     the unload animation. Only a page manager handling page transitions should do this.
    /// </summary>
    public bool IsUnloading { get; set; } = false;

    /// <summary>
    ///     Default constructor.
    /// </summary>
    public AppPage()
    {
        // Set the load/unload animation parameters for this page.
        UseLoadUnloadAnimation = true;
        LoadUnloadAnimationParams = new PageAnimationParameters(
            LoadAnimation: PageAnimationStyle.SlideAndFadeInFromBottom,
            UnloadAnimation: PageAnimationStyle.SlideAndFadeOutToBottom,
            Duration: 0.3f,
            SlideDistance: 25
        );

        // Listen out for the page loading event.
        Loaded += async (sender, e) => await OnLoadedAsync(sender, e);
    }

    /// <summary>
    ///     On page loaded callback method. 
    ///     This triggers whenever the page gets loaded into an element.
    /// </summary>
    protected async Task OnLoadedAsync(object sender, RoutedEventArgs e)
    {
        // When loaded gets triggered, do the reverse and unload instead, if requested ...
        if (IsUnloading) // when the page is marked to transition out
        {
            // Animate the page out.
            await AnimatePageUnloadAsync();
        }
        // Otherwise, standard procedure to load the page in ...
        else
        {
            // Animate the page in.
            await AnimatePageLoadAsync();
        }
    }

    /// <summary>
    ///     Animate the loading of the page (transition in).
    /// </summary>
    /// <returns>Task to be awaited.</returns>
    protected async Task AnimatePageLoadAsync() => await (LoadUnloadAnimationParams.LoadAnimation switch
    {
        PageAnimationStyle.None => Task.CompletedTask,

        PageAnimationStyle.SlideAndFadeInFromBottom => this.SlideAndFadeInAsync(
            direction: AnimationSlideDirection.Bottom,
            duration: LoadUnloadAnimationParams.Duration,
            distance: LoadUnloadAnimationParams.SlideDistance,
            skip: !UseLoadUnloadAnimation
        ),

        _ => Task.CompletedTask,
    });

    /// <summary>
    ///     Animate the unloading of the page (transition out).
    /// </summary>
    /// <returns>Task to be awaited.</returns>
    protected async Task AnimatePageUnloadAsync() => await (LoadUnloadAnimationParams.UnloadAnimation switch
    {
        PageAnimationStyle.None => Task.CompletedTask,

        PageAnimationStyle.SlideAndFadeOutToBottom => this.SlideAndFadeOutAsync(
            direction: AnimationSlideDirection.Bottom,
            duration: LoadUnloadAnimationParams.Duration,
            distance: LoadUnloadAnimationParams.SlideDistance,
            skip: !UseLoadUnloadAnimation,
            collapse: true
        ),

        _ => Task.CompletedTask,
    });
}

/// <summary>
///     A base page for all pages to gain base navigation functionality.
/// </summary>
/// <remarks>
///     This class is intended to be directly inherited by any page used in navigation and following the MVVM pattern.
///     See the code snippets below for an example of how to use this class in your project.
///     <code>
///     &lt;nav:AppPage
///         xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
///         xmlns:local="clr-namespace:MyProjectNamespace.Pages.Home"
///         xmlns:nav="clr-namespace:MyProjectNamespace.Core.Navigation.Views"
///         x:TypeArguments="local:HomePageViewModel"&gt;
///     &lt;/nav:AppPage&gt;
///     </code>
///     The code below shows how to inherit from this class in your Code-Behind view.
///     <code>
///     partial class HomePageView : AppPage&lt;HomePageViewModel&gt; { ... }
///     </code>
///     To make the view model work, you need to implement the <see cref="IPageViewModel"/> interface in your view model class.
///     <code>
///     class HomePageViewModel : ObservableViewModel, IPageViewModel { ... }
///     </code>
/// </remarks>
public abstract class AppPage<TViewModel> : AppPage
    where TViewModel : IPageViewModel, new()
{
    /// <summary>
    ///     Reference to the service that can retrieve view models.
    /// </summary>
    private readonly IViewModelProvider _viewModelProvider;

    /// <summary>
    ///     The view model associated with this page.
    /// </summary>
    public TViewModel ViewModel
    {
        get => (TViewModel)ViewModelObject!; // supress, we made sure in the constructor it is properly set
        set => ViewModelObject = value;
    }

    /// <summary>
    ///     Default constructor.
    /// </summary>
    public AppPage() : base()
    {
        // Get the view model provider from the service collection.
        _viewModelProvider = App.Current.Services.GetRequiredService<IViewModelProvider>();

        // If we are in design mode, we can just use a blank instance of the view model.
        if (DesignerProperties.GetIsInDesignMode(this))
        {
            ViewModel = new TViewModel();
        }
        // Otherwise, get the view model from the provider.
        else
        {
            ViewModel = _viewModelProvider.GetRequired<TViewModel>();
        }
    }

    /// <summary>
    ///     Constructor with the ability to initialize the page with the already set view model.
    /// </summary>
    /// <param name="givenViewModel">The specific view model to use.</param>
    public AppPage(TViewModel givenViewModel) : this()
    {
        if (givenViewModel is not null)
            ViewModel = givenViewModel;
    }
}
