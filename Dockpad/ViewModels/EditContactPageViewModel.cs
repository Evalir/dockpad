using Dockpad.Models;
using Dockpad.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dockpad.ViewModels
{
    class EditContactPageViewModel : INavigationAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Contact Form { get; set; } = new Contact();

        private IAPIManager _apiManager { get; set; }

        private INavigationService _navigationService;

        public DelegateCommand SaveCommand { get; set; }

        public EditContactPageViewModel(INavigationService navigationService, IAPIManager apiManager)
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
            throw new NotImplementedException();
        }

        public async void ExecuteSaveCommand()
        {
            Form.Pets = "";
            Form.FoodPreferences = "";
            var response = await _apiManager.PostContact(Config.Token, Form);
            if (response.IsSuccessStatusCode)
            {
                await _navigationService.GoBackAsync();
            } else
            {
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine();
            }
        }
    }
}
