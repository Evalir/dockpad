﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    class MoodPageViewModel : BaseViewModel
    {

        public ObservableCollection<Mood> Moods { get; set; } = new ObservableCollection<Mood>();
        public DelegateCommand AddMoodCommand { get; set; }

        public DelegateCommand RefreshCommand { get; set; }

        public bool IsRefreshing { get; set; }

        private IAPIManager _apiManager { get; set; }

        private IPageDialogService _pageDialog;

        private static Dictionary<string, string> _moodDict = new Dictionary<string, string>(){
            {"5", "Happy"},
            {"4", "Good" },
            {"3", "Netural" },
            {"2", "Bad" },
            {"1", "Sad" }
        };

        public MoodPageViewModel(INavigationService navigationService, IAPIManager apiManager, IPageDialogService pageDialog) : base(navigationService)
        {
            _pageDialog = pageDialog;
            _apiManager = apiManager;
            AddMoodCommand = new DelegateCommand(AddMood);
            RefreshCommand = new DelegateCommand(ExecuteRefresh);
            LoadMoods();
        }

        private async void AddMood()
        {
            await NavigateToAsync(new Uri(NavigationConstants.EditMoodPage, UriKind.Relative));
        }

        private async void LoadMoods()
        {
            var response = await _apiManager.GetMoods(Config.Token);
            if (response.IsSuccessStatusCode)
            {
                Moods.Clear();
                var json = await response.Content.ReadAsStringAsync();
                PaginatedResponse<Mood> moods = await Task.Run(() => JsonConvert.DeserializeObject<PaginatedResponse<Mood>>(json));
                moods.Results.Reverse();
                foreach (var mood in moods.Results)
                {
                    mood.mood = $"{mood.Date} - {_moodDict[mood.mood]}";
                    Moods.Add(mood);
                }
            }
        }

        private void ExecuteRefresh()
        {
            LoadMoods();
            IsRefreshing = false;
        }
    }
}
