using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dockpad.Helpers;
using Dockpad.Models;
using Dockpad.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Dockpad.ViewModels
{
    class ContactsPageViewModel : BaseViewModel
    {
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();

        private Contact _selectedContact;

        public bool IsRefreshing { get; set; }

        public Contact SelectedContact { get => _selectedContact; 
            set { 
                _selectedContact = value ;
                if (_selectedContact == null)
                    return;
                ExecuteSelectContact(_selectedContact);
                SelectedContact = null;
            } 
        }
        public DelegateCommand ViewContactCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand AddContactCommand { get; set; }

        private IAPIManager _apiManager;
        private INavigationService _navigationService;
        private IPageDialogService _pageDialog;
        public ContactsPageViewModel(INavigationService navigationService, IAPIManager apiManager, IPageDialogService pageDialog) : base(navigationService)
        {

            _apiManager = apiManager;
            _navigationService = navigationService;
            _pageDialog = pageDialog;

            AddContactCommand = new DelegateCommand(ExecuteAddContact);
            RefreshCommand = new DelegateCommand(ExecuteRefresh);
            LoadContacts();
        }

        private async void ExecuteViewContact()
        {
            await NavigateToAsync(new Uri(NavigationConstants.HomePage, UriKind.Relative));
        }

        public async void ExecuteSelectContact(Contact contact)
        {
            string message = $"{contact.FirstName} {contact.LastName}\n" +
                $"Email: {contact.Email}\n" +
                $"Nick name: {contact.Nickname}\n" +
                $"Phone number: {contact.PhoneNumber}\n" +
                $"Address: {contact.Address}\n" +
                $"Phone Met: {contact.Met}";

            var more = await _pageDialog.DisplayAlertAsync("Contact details", message, "More", "Ok");
            if (more)
            {
                string action = await _pageDialog.DisplayActionSheetAsync("What would you like to do?", "Cancel", null, "Edit", "Delete");

                if (action == "Delete")
                {
                    int idx = Contacts.IndexOf(contact);
                    await _apiManager.DeleteContact(Config.Token, contact.Code);
                    Contacts.RemoveAt(idx);
                }
                else if (action == "Edit")
                {
                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("contact", contact);
                    await _navigationService.NavigateAsync(new Uri(NavigationConstants.EditContactPage, UriKind.Relative), navigationParams);
                }
            }
        }

        public async void LoadContacts()
        {
            var response = await _apiManager.GetContacts(Config.Token);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                PaginatedResponse<Contact> contacts = await Task.Run(() => JsonConvert.DeserializeObject<PaginatedResponse<Contact>>(json));
                foreach(Contact c in contacts.Results)
                {
                    Contacts.Add(c);
                }
            }
        }

        public async void ExecuteAddContact()
        {
            await _navigationService.NavigateAsync(new Uri(NavigationConstants.EditContactPage, UriKind.Relative));
        }

        public async void ExecuteRefresh()
        {
            Contacts.Clear();
            LoadContacts();       
            IsRefreshing = false;
        }
    }
}
