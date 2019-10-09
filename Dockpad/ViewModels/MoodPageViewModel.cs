using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Dockpad.Models;
using Prism.Commands;
using Prism.Navigation;

namespace Dockpad.ViewModels
{
    class MoodPageViewModel : BaseViewModel
    {

        public ObservableCollection<Mood> Moods { get; set; }
        public DelegateCommand AddMoodCommand { get; set; }

        public MoodPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Moods = new ObservableCollection<Mood>()
            {
                new Mood("Me", "Great", "Very Duro")
            };
            AddMoodCommand = new DelegateCommand(AddMood);
        }

        private async void AddMood()
        {
            await NavigateToAsync(new Uri(NavigationConstants.AddMoodPage, UriKind.Relative));
        }
    }
}
