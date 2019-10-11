using Dockpad.Models;
using Dockpad.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace Dockpad.ViewModels
{
    class EditContactPageViewModel : INavigationAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Contact Form { get; set; } = new Contact();

        private IAPIManager _apiManager { get; set; }

        private INavigationService _navigationService;

        private IPageDialogService _pageDialog;

        public DelegateCommand SaveCommand { get; set; }

        private bool _is_new = true;

        public EditContactPageViewModel(INavigationService navigationService, IAPIManager apiManager, IPageDialogService pageDialog)
        {
            _apiManager = apiManager;
            _navigationService = navigationService;
            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("contact"))
            {
                _is_new = false;
                Contact contact = (Contact)parameters["contact"];
                Form = contact;
            }
        }

        public async void ExecuteSaveCommand()
        {
            Form.Pets = "";
            Form.FoodPreferences = "";
            HttpResponseMessage response;

            if (_is_new)
            {
                response = await _apiManager.PostContact(Config.Token, Form);
            } else
            {
                response = await _apiManager.PatchContact(Config.Token, Form.Code, Form);
            }

            if (response.IsSuccessStatusCode)
            {
                await _navigationService.GoBackAsync();
            } else
            {
                await _pageDialog.DisplayAlertAsync("An error ocurred", "An error ocurrer while trying to save the contact", "ok");
            }
        }
    }
}
