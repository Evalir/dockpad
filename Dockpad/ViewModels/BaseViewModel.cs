using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Dockpad.Services;
using Prism.Navigation;

namespace Dockpad.ViewModels
{
    public class BaseViewModel : INavigationAware, INotifyPropertyChanged
    {
        IAPIService<IPRMAPI> prmApi = new APIService<IPRMAPI>(Config.DomainURL);

        protected INavigationService _navigationService { get; }

        public event PropertyChangedEventHandler PropertyChanged;


        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        /// <summary>
        /// Navigates to the specified Uri.
        /// </summary>
        /// <param name="Page">Page to navigate to.</param>
        /// <returns>Task</returns>
        protected Task NavigateToAsync(Uri Page)
        {
            return _navigationService.NavigateAsync(Page);
        }

        /// <summary>
        /// Returns to the previous page contained in the navigation stack.
        /// </summary>
        /// <returns>Task</returns>
        protected Task GoBackAsync()
        {
            return _navigationService.GoBackAsync();
        }

        /// <summary>
        /// Returns to the root page contained in the navigation stack.
        /// </summary>
        /// <returns>Task</returns>
        protected Task GoBackToRootAsync()
        {
            return _navigationService.GoBackToRootAsync();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            // Handle code to be executed on entering from a page
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            // Handle code to be executed on entering to a page.
        }
    }
}
