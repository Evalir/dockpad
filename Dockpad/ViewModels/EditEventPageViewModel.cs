﻿using Acr.UserDialogs;
using Dockpad.Models;
using Dockpad.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace Dockpad.ViewModels
{
    class EditEventPageViewModel : INavigationAware, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private IAPIManager _apiManager;
        private INavigationService _navigationService;
        IPageDialogService _pageDialog;

        IUserDialogs _userDialogs = UserDialogs.Instance;

        public TimeSpan StartTime { get; set; } = new TimeSpan(12, 0, 0);
        public TimeSpan EndTime { get; set; } = new TimeSpan(12, 0, 0);

        public DateTime SelectedDate { get; set; }  = DateTime.UtcNow;

        public Event Form { get; set; } = new Event();

        public DelegateCommand SaveCommand { get; set; }

        // Handle wether this is a new event or is editing an existing one
        private bool _is_new = true;

        public EditEventPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IAPIManager apiManager) 
        {
            _navigationService = navigationService;
            _pageDialog = pageDialog;
            _apiManager = apiManager;
            SaveCommand = new DelegateCommand(ExecuteSave);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("event"))
            {
                _is_new = false;
                Event ev = (Event)parameters["event"];
                Form = ev;
                StartTime = TimeSpan.Parse(ev.StartTime);
                EndTime = TimeSpan.Parse(ev.EndTime);

                string formatter = "yyyy-MM-dd";
                SelectedDate = DateTime.ParseExact(ev.Date, formatter,
                                                        CultureInfo.InvariantCulture);

            }
        }

        private async void ExecuteSave()
        {
            Form.StartTime = StartTime.ToString();
            Form.EndTime = EndTime.ToString();
            Form.Date = SelectedDate.ToString("yyyy-MM-dd");
            HttpResponseMessage response;

            if (Form.Title == null || Form.Description == null || Form.Location == null)
            {
                _userDialogs.Toast("Please fill all the fields", TimeSpan.FromSeconds(3));
                return;
            }

            if (_is_new)
            {
                response = await _apiManager.PostEvent(Config.Token, Form);
            }
            else
            {
                response = await _apiManager.PatchEvent(Config.Token, Form.Code, Form);
            }
          
            if (response.IsSuccessStatusCode)
            {
                _userDialogs.Toast("Event successfully saved. Please reload the calendar to reflect the changes", TimeSpan.FromSeconds(3));
                await _navigationService.GoBackAsync();
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            // Handle cade for navigated from.
        }

    }
}
