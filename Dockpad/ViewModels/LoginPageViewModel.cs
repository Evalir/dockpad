using Dockpad.Forms;
using Dockpad.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Dockpad.Services;
using System.Threading.Tasks;

namespace Dockpad.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        INavigationService _navigationService;

        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand LogInCommand { get; set; }
        public DelegateCommand RegisterCommand { get; set; }

        PRMAPIService API { get; set; }

        public LoginForm Form { get; set; }

        public LoginPageViewModel(INavigationService navigationService) 
        {
            Form = new LoginForm();
            API = new PRMAPIService();
            _navigationService = navigationService;
            LogInCommand = new DelegateCommand(ExecuteLogin);
            RegisterCommand = new DelegateCommand(ExecuteRegister);
        }

        private void ExecuteRegister()
        {
            _navigationService.NavigateAsync(new Uri("/RegisterPage", UriKind.Relative));
        }

       /* private bool CanExecuteRegister()
        {
            throw new NotImplementedException();
        }*/

        private async void ExecuteLogin()
        {
            User user = await API.Login(Form);
            if (user != null)
            {
                // TODO: Resolve where the user data will be handled or saved
                Config.Token = user.Token;
                await _navigationService.NavigateAsync(new Uri("/HomePage", UriKind.Relative));
            }
        }

        /*private bool CanExecuteLogin()
        {
            throw new NotImplementedException();
        }*/
    }
}
