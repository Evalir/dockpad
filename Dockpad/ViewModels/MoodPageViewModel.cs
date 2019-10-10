﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    class MoodPageViewModel : BaseViewModel
    {

        public ObservableCollection<Mood> Moods { get; set; } = new ObservableCollection<Mood>();
        public DelegateCommand AddMoodCommand { get; set; }

        private IAPIManager _apiManager { get; set; }

        private Dictionary<string, string> _moodDict = new Dictionary<string, string>(){
            {"1", "Happy"},
            {"2", "Good" },
            {"3", "Netural" },
            {"4", "Bad" },
            {"5", "Sad" }
        };

        public MoodPageViewModel(INavigationService navigationService, IAPIManager apiManager) : base(navigationService)
        {
            _apiManager = apiManager;
            AddMoodCommand = new DelegateCommand(AddMood);
            SetUpMoods();
        }

        private async void AddMood()
        {
            await NavigateToAsync(new Uri(NavigationConstants.AddMoodPage, UriKind.Relative));
        }

        private async void SetUpMoods()
        {
            var response = await _apiManager.GetMoods(Config.Token);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                PaginatedResponse<Mood> moods = await Task.Run(() => JsonConvert.DeserializeObject<PaginatedResponse<Mood>>(json));
                foreach (var mood in moods.Results)
                {
                    mood.mood = _moodDict[mood.mood];
                    Moods.Add(mood);
                }
            }
            else
            {
//                await _pageDialog.DisplayAlertAsync("Error", "There was an error retrieving your mood history, please verify your internet conection", "OK");
            }

        }
    }
}
