using System;
using Dockpad.Models;
using Prism.Navigation;

namespace Dockpad.ViewModels
{
    public class ContactDetailPageViewModel : BaseViewModel, INavigatedAware
    {
        public Contact ActiveContact { get; set; }
        public ContactDetailPageViewModel(INavigationService navigationService): base(navigationService)
        {
        }
    }
}
