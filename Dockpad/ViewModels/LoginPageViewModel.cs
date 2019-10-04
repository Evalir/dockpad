using Dockpad.Forms;
using Dockpad.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.ComponentModel;
using Dockpad.Services;
using Dockpad.Helpers;

namespace Dockpad.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        INavigationService _navigationService;

        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand LogInCommand { get; set; }
        public DelegateCommand RegisterViewCommand { get; set; }

        public string Errors { get; set; }

        PRMAPIService API { get; set; }

        public LoginForm Form { get; set; }

        public LoginPageViewModel(INavigationService navigationService) 
        {
            Form = new LoginForm();
            API = new PRMAPIService();
            _navigationService = navigationService;
            LogInCommand = new DelegateCommand(ExecuteLogin);
            RegisterViewCommand = new DelegateCommand(ExecuteRegister);
        }

        private async void ExecuteRegister()
        {
            await _navigationService.NavigateAsync(new Uri("RegisterPage", UriKind.Relative));
        }

        private async void ExecuteLogin()
        {
            Response<User> response = await API.Login(Form);
            if (response.ErrorData == null)
            {
                // TODO: Resolve where the user data will be handled or saved
                Config.Token = $"Token {response.Data.Token}";
                await _navigationService.NavigateAsync(new Uri("HomePage", UriKind.Relative));
            } else if (response.ErrorData.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                // Wrong username/password
                Errors = "Incorrect username or password";
            }
        }
    }
}
