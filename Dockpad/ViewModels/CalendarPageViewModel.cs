using Dockpad.Helpers;
using Dockpad.Models;
using Dockpad.Services;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Dockpad.ViewModels
{
    public class CalendarPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Meeting> Events { get; set; }

        INavigationService _navigationService;

        IAPIManager _apiManager;
        
        public CalendarPageViewModel(INavigationService navigationService, IAPIManager apiManager)
        {
            Events = new ObservableCollection<Meeting>();
            _navigationService = navigationService;
            _apiManager = apiManager;
            SetUpEvents();
        }

        private async void SetUpEvents()
        {
            var eventsResponse = await _apiManager.GetEvents(Config.Token);
            if (eventsResponse.IsSuccessStatusCode)
            {
                var json = await eventsResponse.Content.ReadAsStringAsync();
                PaginatedResponse<Event> events = await Task.Run(() => JsonConvert.DeserializeObject<PaginatedResponse<Event>>(json));
                foreach (Event e in events.Results)
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
