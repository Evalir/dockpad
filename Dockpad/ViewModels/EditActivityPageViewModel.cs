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
    public class EditActivityPageViewModel : INavigationAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Activity Form { get; set; } = new Activity();

        private IAPIManager _apiManager { get; set; }

        private INavigationService _navigationService;

        private IPageDialogService _pageDialog;

        public DelegateCommand SaveCommand { get; set; }

        private bool _is_new = false;

        public EditActivityPageViewModel(INavigationService navigationService, IAPIManager apiManager, IPageDialogService pageDialog)
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
            if (parameters.ContainsKey("activity"))
            {
                _is_new = true;
                Activity activity = (Activity)parameters["activity"];
                Form = activity;
            }
        }

        public async void ExecuteSaveCommand()
        {
         
            HttpResponseMessage response;

            if (_is_new)
            {
                response = await _apiManager.PatchActivity(Config.Token, Form, Form.Code);
            }
            else
            {
                response = await _apiManager.PostActivity(Config.Token, Form);
            }

            if (response.IsSuccessStatusCode)
            {
                await _navigationService.GoBackAsync();
            }
            else
            {
                await _pageDialog.DisplayAlertAsync("An error ocurred", "An error ocurrer while trying to save the activity", "ok");
            }
        }
    }
}
