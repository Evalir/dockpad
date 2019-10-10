using Dockpad.Models;
using Dockpad.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dockpad.ViewModels
{
    public class EditProfilePageViewModel : INavigationAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private IAPIManager _apiManager;

        public DelegateCommand SaveCommand { get; set; }

        public User User { get; set; } = new User();
        public Profile Profile { get; set; } = new Profile();

        private INavigationService _navigationService;

        private IPageDialogService _pageDialog;

        public EditProfilePageViewModel(INavigationService navigationService, IAPIManager apiManager, IPageDialogService pageDialog)
        {
            _apiManager = apiManager;
            _navigationService = navigationService;
            _pageDialog = pageDialog;
            SaveCommand = new DelegateCommand(ExecuteSaveCommand);
        }
        private async void ExecuteSaveCommand()
        {
           var answer = await _pageDialog.DisplayAlertAsync("Update profile", "Would you like to continue with this action?", "ok", "Cancel");
           if(answer)
            {

                User.Profile.Picture = null;
                Profile.Picture = null;
                var name = User.Username;
;               var responseUser = await _apiManager.PatchUser(Config.Token, name, User);
                var responseProfile = await _apiManager.PatchProfile(Config.Token, Profile);

                if (responseUser.IsSuccessStatusCode && responseProfile.IsSuccessStatusCode)
                {
                    await _pageDialog.DisplayAlertAsync("Success", "You have updated your profile", "ok");
                    await _navigationService.NavigateAsync(new Uri(NavigationConstants.HomePage, UriKind.Absolute));
                }
            }
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            User = parameters.GetValue<User>("user");
            Profile = parameters.GetValue<Profile>("profile");
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
