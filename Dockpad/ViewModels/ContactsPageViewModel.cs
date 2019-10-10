using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Dockpad.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Dockpad.ViewModels
{
    class ContactsPageViewModel : BaseViewModel
    {
        public ObservableCollection<Contact> Contacts { get; set; }
        public Contact SelectedContact { get; set; }
        public DelegateCommand ViewContactCommand { get; set; }
        public DelegateCommand DeleteContactCommand { get; set; }
        public ContactsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Contacts = new ObservableCollection<Contact>()
            {
                new Contact("Enrique", "enriqueortizpi@gmail.com"),
                new Contact("Albin", "albinest@gmail.com")
            };
        }

        private async void ExecuteViewContact()
        {
            NavigateToAsync(new Uri(NavigationConstants.HomePage, UriKind.Relative));
        }

        public async void ExecuteDeleteContact()
        {

        }
    }
}
