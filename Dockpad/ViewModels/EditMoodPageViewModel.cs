using System;
using System.Collections.Generic;
using System.ComponentModel;
using Acr.UserDialogs;
using Dockpad.Models;
using Dockpad.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace Dockpad.ViewModels
{
    public class EditMoodPageViewModel : BaseViewModel
    {

        private IAPIManager _apiManager;

        IUserDialogs _userDialogs = UserDialogs.Instance;
        
        public DateTime Today { get; set; } = DateTime.UtcNow;

        public DateTime SelectedDate { get; set; } = DateTime.UtcNow;

        public string SelectedMood { get; set; }
        public DelegateCommand<object> SelectMoodCommand { get; set; }
        public DelegateCommand SaveMoodCommand { get; set; }

        public Mood Form { get; set; } = new Mood();

        private Dictionary<string, string> _moodDict = new Dictionary<string, string>(){
            {"Happy", "5"},
            {"Good", "4"},
            {"Neutral", "3"},
            {"Bad", "2"},
            {"Sad", "1" }
        };

        public EditMoodPageViewModel(INavigationService navigationService, IAPIManager aPIManager) : base(navigationService)
        {
            _apiManager = aPIManager;
            SelectMoodCommand = new DelegateCommand<object>(SelectMood);
            SaveMoodCommand = new DelegateCommand(ExecuteSaveMood);
        }

        private void SelectMood(object moodStatus)
        {
            var moodType = Convert.ToString(moodStatus);
            string rate = _moodDict[moodType];
            SelectedMood = moodType;
            Form.mood = rate;
        }

        private async void ExecuteSaveMood()
        {
            if (Form.Description == null || Form.Hightlights == null)
            {
                _userDialogs.Toast("Please fill all the fields", TimeSpan.FromSeconds(3));
                return;
            }

            Form.Date = SelectedDate.ToString("yyyy-MM-dd");
            var response = await _apiManager.PostMood(Config.Token, Form);
            if (response.IsSuccessStatusCode)
            {
                await _navigationService.GoBackAsync();   
            }
        }
    }
}
