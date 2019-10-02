using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Dockpad.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        INavigationService _navigationService;
        public DelegateCommand LogInCommand { get; set; }
        public DelegateCommand RegisterCommand { get; set; }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        public LoginPageViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
            LogInCommand = new DelegateCommand(ExecuteLogin).ObservesProperty(() => Email).ObservesProperty(() => Password);
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

        private void ExecuteLogin()
        {
            throw new NotImplementedException();
        }

        /*private bool CanExecuteLogin()
        {
            throw new NotImplementedException();
        }*/
    }
}
