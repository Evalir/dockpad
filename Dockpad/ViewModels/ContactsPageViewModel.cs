using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Dockpad.Helpers;
using Dockpad.Models;
using Dockpad.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace Dockpad.ViewModels
{
    class ContactsPageViewModel : BaseViewModel
    {
        public ObservableCollection<Contact> Contacts { get; set; } = new ObservableCollection<Contact>();
        public Contact SelectedContact { get; set; }
        public DelegateCommand ViewContactCommand { get; set; }
        public DelegateCommand DeleteContactCommand { get; set; }

        private IAPIManager _apiManager;
        public ContactsPageViewModel(INavigationService navigationService, IAPIManager apiManager) : base(navigationService)
        {
            _apiManager = apiManager;
            LoadContacts();
        }

        private async void ExecuteViewContact()
        {
            await NavigateToAsync(new Uri(NavigationConstants.HomePage, UriKind.Relative));
        }

        public async void ExecuteDeleteContact()
        {

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
    }
}
