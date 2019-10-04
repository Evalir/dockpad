using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Dockpad.Models;
using Prism.Navigation;

namespace Dockpad.ViewModels
{
    class MoodPageViewModel : INotifyPropertyChanged
    {
        INavigationService _navigationService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Mood> Moods { get; set; }

        public MoodPageViewModel()
        {
            Moods = new ObservableCollection<Mood>()
            {
                new Mood("Enrique Ortiz", "Great", "Did stuff")
            };
        }
    }
}
