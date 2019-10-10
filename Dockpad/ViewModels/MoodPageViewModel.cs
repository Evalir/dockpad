using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
            SetUpMoods();
        }

        private async void AddMood()
        {
            await NavigateToAsync(new Uri(NavigationConstants.EditMoodPage, UriKind.Relative));
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
                    mood.mood = $"{mood.Date} - {_moodDict[mood.mood]}";
                    Moods.Add(mood);
                }
            }
            else
            {
                await _pageDialog.DisplayAlertAsync("Error", "There was an error retrieving your mood history, please verify your internet conection", "OK");
            }

        }
    }
}
