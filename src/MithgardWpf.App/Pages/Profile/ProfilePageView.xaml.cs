// Author: Tomáš Frixs

using MithgardWpf.App.Core.Navigation.Views;

namespace MithgardWpf.App.Pages.Profile;

/// <summary>
/// Interaction logic for ProfilePageView.xaml
/// </summary>
public partial class ProfilePageView : AppPage<ProfilePageViewModel>
{
    public ProfilePageView()
    {
        InitializeComponent();
    }

    public ProfilePageView(ProfilePageViewModel givenViewModel) : base(givenViewModel)
    {
        InitializeComponent();
    }
}
