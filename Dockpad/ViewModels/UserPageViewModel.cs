using Dockpad.Models;
using Prism.Navigation;
using Dockpad.Services;
using Dockpad.Helpers;
using System.ComponentModel;
using Prism.Mvvm;
using System.Collections.Generic;

namespace Dockpad.ViewModels
{
    public class UserPageViewModel : BaseViewModel
    {
        public User User { get; set; } = new User();
        public Profile Profile { get; set; } = new Profile();

        public List<Event> Events { get; set; }
        public UserPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            API = new PRMAPIService();
            LoadProfile();
            LoadEvents();
        }

        private async void LoadProfile()
        {

            Response<User> response = await API.GetProfile();
            if (response.ErrorData == null)
            {
                User = response.Data;
                Profile = User.Profile;
            }
            else if (response.ErrorData.StatusCode == System.Net.HttpStatusCode.BadRequest)
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
