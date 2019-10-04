using System;
using Prism.Navigation;

namespace Dockpad.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        
        public HomePageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            // Can receive parameters here
            throw new NotImplementedException();
            
        }
    }
}
