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
    public class LoginPageViewModel : BaseViewModel
    {

        public DelegateCommand LogInCommand { get; set; }
        public DelegateCommand RegisterViewCommand { get; set; }

        public string Errors { get; set; }

        public LoginForm Form { get; set; }

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Form = new LoginForm();
            API = new PRMAPIService();
            LogInCommand = new DelegateCommand(ExecuteLogin);
            RegisterViewCommand = new DelegateCommand(ExecuteRegister);
        }

        private async void ExecuteRegister()
        {
            await NavigateToAsync(new Uri(NavigationConstants.RegisterPage, UriKind.Relative));
        }

        private async void ExecuteLogin()
        {
            Response<User> response = await API.Login(Form);
            if (response.ErrorData == null)
            {
                // TODO: Resolve where the user data will be handled or saved
                Config.Token = $"Token {response.Data.Token}";
                await NavigateToAsync(new Uri(NavigationConstants.HomePage, UriKind.Absolute));
            } else if (response.ErrorData.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                // Wrong username/password
                Errors = "Incorrect username or password";
            }
        }
    }
}
