using Dockpad.Models;
using Prism.Navigation;
using Dockpad.Services;
using Dockpad.Helpers;
using System.ComponentModel;
using Prism.Mvvm;

namespace Dockpad.ViewModels
{
    public class UserPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        INavigationService _navigationService;
        PRMAPIService API { get; set; }
        public User User { get; set; } = new User();
        public Profile Profile { get; set; } = new Profile();
        public UserPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            API = new PRMAPIService();
            LoadProfile();
        }

        public async void LoadProfile()
        {
            Response<User> response = await API.GetProfile();
            if (response.ErrorData == null)
            {
               User  = response.Data;
               Profile = User.Profile;
            }
            else if (response.ErrorData.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                User.FirstName = "Error getting your profile data";
            }
        }
    }
}
