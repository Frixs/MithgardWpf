// Author: Tomáš Frixs

using MithgardWpf.App.Core.Navigation.Abstractions;
using MithgardWpf.App.Pages.Home;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MithgardWpf.App.Core.Navigation.Views;

/// <summary>
/// Interaction logic for PageHost.xaml
/// </summary>
public partial class PageHost : UserControl
{
    /// <summary>
    ///     Cancellation token used to prevent visual glitches when pages are changed too quickly during transition animations.
    ///     <para/>
    ///     When a new page is loaded before the previous one completes its loading, a race condition may occur. 
    ///     The new page gets assigned to the current page frame correctly. However, the current one is moved 
    ///     to the old page frame before the actual old page finishes its unloading. In fact, the free-up task 
    ///     for the old page frame is already scheduled. Thus, the current page might mistakenly be treated 
    ///     as the "old" page. This token allows cancellation of those outdated removal tasks to avoid visual glitches.
    /// </summary>
    private static int _oldPageRemovalToken = default;

    /// <summary>
    ///     Refernece to the logger.
    /// </summary>
    private static readonly ILogger<PageHost> _logger = App.Current.Services.GetRequiredService<ILogger<PageHost>>();

    /// <summary>
    ///     Reference to the page mapper service that maps page identifiers to their respective view models and views.
    /// </summary>
    private static readonly IPageMapper _pageMapper = App.Current.Services.GetRequiredService<IPageMapper>();

    #region Dependency Properties

    /// <summary>
    ///     Registers <see cref="CurrentPageIdentifier"/> as a dependency property.
    /// </summary>
    public static readonly DependencyProperty CurrentPageIdentifierProperty =
        DependencyProperty.Register(
            nameof(CurrentPageIdentifier), 
            typeof(string), 
            typeof(PageHost), 
            new UIPropertyMetadata(
                default(string), 
                null,
                OnCurrentPageIdentifierPropertyChanged
            )
        );

    /// <summary>
    ///     Registers <see cref="CurrentPageViewModel"/> as a dependency property.
    /// </summary>
    public static readonly DependencyProperty CurrentPageViewModelProperty =
        DependencyProperty.Register(
            nameof(CurrentPageViewModel), 
            typeof(IPageViewModel), 
            typeof(PageHost), 
            new UIPropertyMetadata()
        );

    #endregion

    /// <summary>
    ///     The identifier of the page currently being shown.
    /// </summary>
    public string CurrentPageIdentifier
    {
        get => (string)GetValue(CurrentPageIdentifierProperty);
        set => SetValue(CurrentPageIdentifierProperty, value);
    }

    /// <summary>
    ///     The view model of the page currently being shown.
    /// </summary>
    public IPageViewModel? CurrentPageViewModel
    {
        get => (IPageViewModel?)GetValue(CurrentPageViewModelProperty);
        set => SetValue(CurrentPageViewModelProperty, value);
    }

    /// <summary>
    ///     Default constructor.
    /// </summary>
    public PageHost()
    {
        InitializeComponent();

        // If we are in design mode, we can just use a blank instance of the view model.
        if (DesignerProperties.GetIsInDesignMode(this))
        {
            CurrentPage.Content = _pageMapper.GetNewPageView(HomePageViewModel.PageIdentifier);
        }
    }

    /// <summary>
    ///     The callback event logic when <see cref="CurrentPageIdentifier"/> value gets changed.
    /// </summary>
    /// <param name="d">The UI element that got changes in its attached property of this type.</param>
    /// <param name="value">The new value that got updated.</param>
    /// <returns>The <paramref name="value"/> for chaining.</returns>
    private static object OnCurrentPageIdentifierPropertyChanged(DependencyObject d, object value)
    {
        // Get current values from XAML binding.
        object? newPageIdentifier = value;
        object? newPageViewModel = d.GetValue(CurrentPageViewModelProperty);

        // Get the frames.
        Frame? currentPageFrame = (d as PageHost)?.CurrentPage;
        Frame? oldPageFrame = (d as PageHost)?.OldPage;

        // Do the page swap.
        SwapPage(currentPageFrame, oldPageFrame, newPageIdentifier, newPageViewModel);

        return value;
    }

    /// <summary>
    ///     Procedure to swap UI pages in frames: unload the current page and load the new one provided as an argument.
    /// </summary>
    /// <param name="newPageIdentifierObject">The identifier object of the new page being loaded.</param>
    /// <param name="newPageViewModelObject">The view model object of the new page being loaded if specified explicitly (optional).</param>
    /// <param name="currentPageFrame">The frame of the currently displayed page that must be unloaded.</param>
    /// <param name="oldPageFrame">The frame to which to move the currently displayed page for a smooth unloading process.</param>
    private static void SwapPage(Frame? currentPageFrame, Frame? oldPageFrame, object? newPageIdentifierObject, object? newPageViewModelObject = null)
    {
        // Parameter frame null checks
        if (currentPageFrame is null || oldPageFrame is null)
        {
            _logger.LogError("Current or old page frame is null. Cannot swap pages.");
            return;
        }

        // Check the page identifier is of the correct type and not invalid ...
        if (newPageIdentifierObject is not string newPageIdentifier || string.IsNullOrWhiteSpace(newPageIdentifier))
        {
            _logger.LogError("Invalid or empty page identifier provided. Cannot swap pages.");
            return;
        }

        // Indicates whether the new page should be loaded into the current page frame.
        bool shouldLoadNewPage = true;

        // If there is already existing content in the current page frame, we need to handle unloading if needed.
        // This branch is taken most of the time, except when no page is currently displayed (e.g., during app startup).
        if (currentPageFrame.Content is AppPage currentPage)
        {
            // If the new page is the same as the current one ...
            string currentPageIdentifier = _pageMapper.GetIdentifier(currentPage);
            if (currentPageIdentifier.Equals(newPageIdentifier))
            {
                // The page is the same, no need to load a new one, just update the view model.
                if (newPageViewModelObject is not null)
                {
                    if (_pageMapper.CheckViewModel(currentPageIdentifier, newPageViewModelObject))
                        currentPage.ViewModelObject = newPageViewModelObject;
                    else
                        _logger.LogError(
                            "Provided view model type '{ViewModelType}' does not match expected type for page identifier '{PageIdentifier}'.",
                            newPageViewModelObject.GetType(),
                            currentPageIdentifier
                        );
                }
                shouldLoadNewPage = false;
            }
            // Otherwise, unload the current page and prep for new one ...
            else
            {
                // Grab the frame content
                var oldPageFrameContent = currentPageFrame.Content;
                AppPage oldPage = (AppPage)oldPageFrameContent; // explicit cast is safe here as the frame has the content of the correct type

                // Set the page to play the unload animation when the load event happens.
                oldPage.IsUnloading = true;

                // Move the current page content to the old page frame to be able to unload it smoothly.
                // ... and before, free up the current page frame for the new page.
                currentPageFrame.Content = null;
                oldPageFrame.Content = oldPageFrameContent;

                // At this point, the page still stays the same, but now we changed in which frame it is displayed.
                // The change triggers the page load event (the page was marked for unloading in the code above).
                // Once the unloading animation is done, remove the page, no need to keep it in the frame behind hidden.
                int oldPageRemovalToken = _oldPageRemovalToken = new Random().Next();
                int oldPageRemovalTimeDelay = oldPage.UseLoadUnloadAnimation ? ((int)oldPage.LoadUnloadAnimationParams.Duration * 1000) : 1; // in milliseconds
                Task.Delay(oldPageRemovalTimeDelay).ContinueWith((t) =>
                {
                    if (oldPageRemovalToken == _oldPageRemovalToken)
                    {
                        // Remove old page. It is not needed anymore to stay in the old frame once it fades out completely.
                        Application.Current.Dispatcher.Invoke(() => oldPageFrame.Content = null);
                        _oldPageRemovalToken = default;
                    }
                });
                shouldLoadNewPage = true;
            }
        }

        // Load a new page if requested.
        if (shouldLoadNewPage)
        {
            // Set the new page content.
            currentPageFrame.Content = _pageMapper.GetNewPageView(newPageIdentifier, newPageViewModelObject);
        }

        // So, both pages will eventually load into a new element - the existing current page to the old frame and the new page to the current frame.
        // After the load event fires on both pages, it triggers animations to transition the old page out and the new page in.
    }
}
