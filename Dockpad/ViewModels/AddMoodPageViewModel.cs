using System;
using System.ComponentModel;
using Prism.Commands;
using Prism.Navigation;

namespace Dockpad.ViewModels
{
    public class AddMoodPageViewModel : BaseViewModel
    {

        public string SelectedMood { get; set; }
        public DelegateCommand<object> SelectMoodCommand { get; set; }
        public DelegateCommand SaveMoodCommand { get; set; }

        public AddMoodPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SelectMoodCommand = new DelegateCommand<object>(SelectMood);
        }

        private void SelectMood(object Mood)
        {
            var moodType = Convert.ToString(Mood);
            SelectedMood = moodType;
        }
    }
}
