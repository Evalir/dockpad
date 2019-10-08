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

namespace Dockpad.ViewModels
{
    public class UserPageViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        INavigationService _navigationService;
        public User User { get; set; } = new User();
        public Profile Profile { get; set; } = new Profile();

        private IAPIManager _apiManager;
        public UserPageViewModel(INavigationService navigationService, IAPIManager apiManager)
        {
            _navigationService = navigationService;
            _apiManager = apiManager;
            public User User { get; set; } = new User();
            public Profile Profile { get; set; } = new Profile();

            public List<Event> Events { get; set; }
            LoadProfile();
            LoadEvents();
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
            Response<PaginatedResponse<Event>> response = await API.GetAllEvents();
            if(response.ErrorData == null)
            {
                Events = new List<Event>(response.Data.Results);
            }
            else if(response.ErrorData.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                //Handle error here
            }            
        }

    }
}
