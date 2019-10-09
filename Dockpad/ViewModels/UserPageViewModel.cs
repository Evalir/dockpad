using Dockpad.Models;
using Prism.Navigation;
using Dockpad.Services;
using Dockpad.Helpers;
using System.ComponentModel;
using Prism.Mvvm;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Prism.Commands;

namespace Dockpad.ViewModels
{
    public class UserPageViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public User User { get; set; } = new User();
        public Profile Profile { get; set; } = new Profile();
        public List<Event> Events { get; set; }
        public List<Activity> Activities { get; set; }

        public DelegateCommand EventsCommand { get; set; }

        public DelegateCommand EditProfileCommand { get; set; }

        private IAPIManager _apiManager;

        public UserPageViewModel(INavigationService navigationService, IAPIManager apiManager) :base (navigationService)
        {
            _apiManager = apiManager;

            LoadProfile();
            LoadEvents();
            LoadActivities();
            EventsCommand = new DelegateCommand(ExecuteEventCommand);
            EditProfileCommand = new DelegateCommand(ExecuteEditProfileCommand);
        }
        private async void LoadProfile()
        {
            var profileResponse = await _apiManager.GetProfile(Config.Token);
            if (profileResponse.IsSuccessStatusCode)
            {
                var json = await profileResponse.Content.ReadAsStringAsync();
                var result = JObject.Parse(json);

                User = await Task.Run(() => JsonConvert.DeserializeObject<User>(json));

                // This is a dirty solution, would there be another way to serialized nested json data into an object?
                var profileJson = result["profile"];
                var serializedProfile = JsonConvert.SerializeObject(profileJson, Formatting.Indented);
                Profile = await Task.Run(() => JsonConvert.DeserializeObject<Profile>(serializedProfile));
            }
            else
            {
                User.FirstName = "Error getting your profile data";
            }
        }

        private async void LoadEvents()
        {
            var eventsResponse = await _apiManager.GetEvents(Config.Token);
            if (eventsResponse.IsSuccessStatusCode)
            {
                var json = await eventsResponse.Content.ReadAsStringAsync();
                PaginatedResponse<Event> events = await Task.Run(() => JsonConvert.DeserializeObject<PaginatedResponse<Event>>(json));
                Events = new List<Event>(events.Results);
            }
            else 
            {
                //Handle error here
            }
        }

        private async void LoadActivities()
        {
            var activitiesResponse =  await _apiManager.GetActivities(Config.Token);

            if(activitiesResponse.IsSuccessStatusCode)
            {
                var json = await activitiesResponse.Content.ReadAsStringAsync();
                PaginatedResponse<Activity> activities = await Task.Run(() => JsonConvert.DeserializeObject<PaginatedResponse<Activity>>(json));
                Activities = new List<Activity>(activities.Results);
            }
            else
            {
                //Handle error here
            }
        }

        private async void ExecuteEventCommand()
        {
            await NavigateToAsync(new System.Uri(NavigationConstants.CalendarPage, System.UriKind.Relative));
        }

        private async void ExecuteEditProfileCommand()
        {
            await NavigateToAsync(new System.Uri(NavigationConstants.EditProfilePage, System.UriKind.Relative));
        }

    }
}
