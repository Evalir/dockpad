using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Dockpad.Models;
using Prism.Navigation;

namespace Dockpad.ViewModels
{
    class MoodPageViewModel : BaseViewModel
    {

        public ObservableCollection<Mood> Moods { get; set; }

        public MoodPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Moods = new ObservableCollection<Mood>()
            {
                new Mood("Enrique Ortiz", "Great", "Did stuff"),
                new Mood("Enrique Ortiz", "Good", "Did more stuff")
            };
        }
    }
}
