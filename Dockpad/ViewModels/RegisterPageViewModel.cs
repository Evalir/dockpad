using Dockpad.Forms;
using Dockpad.Helpers;
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
    public class RegisterPageViewModel : INotifyPropertyChanged {
        INavigationService _navigationService;
        IPageDialogService _pageDIalog;
        public DelegateCommand RegisterCommand { get; set; }

        public SignUpForm Form { get; set; }
        PRMAPIService API { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Errors { get; set; }

        public RegisterPageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
        {
            API = new PRMAPIService();
            Form = new SignUpForm();
            _pageDIalog = pageDialog;
            _navigationService = navigationService;
            RegisterCommand = new DelegateCommand(ExecuteRegister);
        }

        private async void ExecuteRegister()
        {
            Response<User> response = await API.SignUp(Form);
            if (response.ErrorData == null)
            {
                // TODO: Resolve where the user data will be handled or saved
                await _pageDIalog.DisplayAlertAsync("You've been succesfully registered", 
                    "Please check your email to verify your account, you will now be redirected to the login page", "ok");
            }
            else if (response.ErrorData.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                // Wrong username/password
                Errors = "Incorrect username or password";
            }
        }
    }
}
