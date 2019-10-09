using Dockpad.Forms;
using Dockpad.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.ComponentModel;
using Dockpad.Services;
using Dockpad.Helpers;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Dockpad.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {

        public DelegateCommand LogInCommand { get; set; }
        public DelegateCommand RegisterViewCommand { get; set; }
        
        public string Errors { get; set; }

        public LoginForm Form { get; set; }

        public IAPIManager _apiManager;

        public LoginPageViewModel(INavigationService navigationService,  IAPIManager manager) : base(navigationService)
        {
            Form = new LoginForm();
            _apiManager = manager;
            LogInCommand = new DelegateCommand(ExecuteLogin);
            RegisterViewCommand = new DelegateCommand(ExecuteRegister);
        }

        private async void ExecuteRegister()
        {
            await NavigateToAsync(new Uri(NavigationConstants.RegisterPage, UriKind.Relative));
        }

        private async void ExecuteLogin()
        {
            var loginResponse = await _apiManager.Login(Form);

            if (loginResponse.IsSuccessStatusCode)
            {
                var json = await loginResponse.Content.ReadAsStringAsync();
                User user = await Task.Run(() => JsonConvert.DeserializeObject<User>(json));
                Config.Token = $"Token {user.Token}";
                var param = new NavigationParameters();
                param.Add("Title", "Home Page");
                
                await _navigationService.NavigateAsync(new Uri(NavigationConstants.HomePage, UriKind.Absolute), param);
            } else
            {
                Errors = "Incorrect username/password";
            }
        }
    }
}
