using System;
using Prism.Navigation;

namespace Dockpad.ViewModels
{
    public class HomePageViewModel: INavigatedAware
    {
        INavigationService _navigationService;
        public HomePageViewModel()
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
