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
        }
    }
}
