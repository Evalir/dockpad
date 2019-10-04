using Dockpad.Helpers;
using Dockpad.Models;
using Dockpad.Services;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Dockpad.ViewModels
{
    public class CalendarPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Meeting> Events { get; set; }

        PRMAPIService API { get; set; }


        INavigationService _navigationService;
        
        public CalendarPageViewModel(INavigationService navigationService)
        {
            Events = new ObservableCollection<Meeting>();
            _navigationService = navigationService;
            API = new PRMAPIService();
            SetUpEvents();
            Console.WriteLine();
        }

        private async void SetUpEvents()
        {
            var response = await API.GetAllEvents();
            if (response.ErrorData == null)
            {
                foreach (Event e in response.Data.Results)
                {
                    string formatter = "yyyy-MM-dd HH:mm:ss";
                    DateTime from = DateTime.ParseExact($"{e.Date} {e.StartTime}", formatter,
                                        CultureInfo.InvariantCulture);
                    DateTime to = DateTime.ParseExact($"{e.Date} {e.EndTime}", formatter,
                                        CultureInfo.InvariantCulture);
                    Meeting meeting = new Meeting { From = from, To = to, EventName = e.Title };
                    Events.Add(meeting);
                }
            }
        }
    }
}
