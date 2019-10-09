using Dockpad.Helpers;
using Dockpad.Models;
using Dockpad.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dockpad.ViewModels
{
    public class CalendarPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public CalendarEventCollection Appointments { get; set; } = new CalendarEventCollection();

        private List<Event> Events { get; set; } = new List<Event>();

        public DelegateCommand<object> CalendarTapCommand { get; set; }

        INavigationService _navigationService;

        IAPIManager _apiManager;

        IPageDialogService _pageDialog;
        
        public CalendarPageViewModel(INavigationService navigationService, IAPIManager apiManager, IPageDialogService pageDialog)
        {
            _navigationService = navigationService;
            _apiManager = apiManager;
            _pageDialog = pageDialog;
            CalendarTapCommand = new DelegateCommand<object>(ExecuteTapCommand);
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
                    CalendarInlineEvent meeting = new CalendarInlineEvent { Subject = e.Title, StartTime = from , EndTime = to};
                    Appointments.Add(meeting);
                    Events.Add(e);
                }
            } 
        }

        private async void ExecuteTapCommand(object obj)
        {
            CalendarInlineEvent appointment = (CalendarInlineEvent)obj;

            int idx = Appointments.IndexOf(appointment);
            Event detailInfo = Events[idx];

            string message = $"{detailInfo.Description}\n" +
                $"From: {appointment.StartTime.ToShortTimeString()}\n" +
                $"To: {appointment.EndTime.ToShortTimeString()}\n" +
                $"Location: {detailInfo.Location}";

            await _pageDialog.DisplayAlertAsync(appointment.Subject, message, "ok");
        }
    }
}
