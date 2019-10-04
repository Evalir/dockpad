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
    public class RegisterPageViewModel : BaseViewModel
    {
        IPageDialogService _pageDialog;
        public DelegateCommand RegisterCommand { get; set; }

        public SignUpForm Form { get; set; }

        public string Errors { get; set; }

        public RegisterPageViewModel(INavigationService navigationService, IPageDialogService pageDialog) : base(navigationService)
        {
            API = new PRMAPIService();
            Form = new SignUpForm();
            _pageDialog = pageDialog;
            RegisterCommand = new DelegateCommand(ExecuteRegister);
        }

        private async void ExecuteRegister()
        {
            Response<User> response = await API.SignUp(Form);
            if (response.ErrorData == null)
            {
                // TODO: Resolve where the user data will be handled or saved
                await _pageDialog.DisplayAlertAsync("You've been succesfully registered",
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
