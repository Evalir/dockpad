using Dockpad.ViewModels;
using Dockpad.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;
using Xamarin.Forms;

namespace Dockpad
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(new System.Uri(NavigationConstants.LoginPage, System.UriKind.Absolute));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<UserPage, UserPageViewModel>();
            containerRegistry.RegisterForNavigation<ContactsPage, ContactPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivityPage, ActivityPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivityLogPage, ActivityLogPageViewModel>();
            containerRegistry.RegisterForNavigation<MoodPage, MoodPageViewModel>();
            containerRegistry.RegisterForNavigation<AddMoodPage, AddMoodPageViewModel>();
        }
    }
}
