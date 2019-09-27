using Dockpad.ViewModels;
using Dockpad.Views;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Prism.Modularity;

namespace Dockpad
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(new System.Uri("/HomePage", System.UriKind.Absolute));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<UserPage, UserPageViewModel>();
            containerRegistry.RegisterForNavigation<ContactsPage, ContactPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivityPage, ActivityPageViewModel>();
            containerRegistry.RegisterForNavigation<ActivityLogPage, ActivityLogPageViewModel>();
            containerRegistry.RegisterForNavigation<MoodsPage, MoodsPage>();


        }
    }
}
