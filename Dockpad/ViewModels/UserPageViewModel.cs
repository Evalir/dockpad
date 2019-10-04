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
        public User Profile { get; set; } = new User();
        
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
                Profile = response.Data;
                
            }
            else if (response.ErrorData.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Profile.FirstName = "Error getting your profile data";
            }
        }
    }
}
