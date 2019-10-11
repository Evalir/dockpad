using Acr.UserDialogs;
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

        IUserDialogs _userDialogs = UserDialogs.Instance;

        public SignUpForm Form { get; set; }

        public string Errors { get; set; }

        private IAPIManager _apiManager;

        public RegisterPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IAPIManager apiManager) : base(navigationService)
        {
            Form = new SignUpForm();
            _apiManager = apiManager;
            _pageDialog = pageDialog;
            RegisterCommand = new DelegateCommand(ExecuteRegister);
        }

        private async void ExecuteRegister()
        {
            if (Form.Email == null || Form.Username == null || Form.FirstName == null || Form.LastName == null)
            {
                _userDialogs.Toast("Please fill all the required fields: names, email and username", TimeSpan.FromSeconds(3));
                return;
            } else if (String.IsNullOrEmpty(Form.Password) || (Form.Password != Form.PasswordConfirmation))
            {
                _userDialogs.Toast("Passwords don't match", TimeSpan.FromSeconds(3));
                return;
            }

            var response = await _apiManager.SignUp(Form);
            if (response.IsSuccessStatusCode)
            {
                // TODO: Resolve where the user data will be handled or saved
                await _pageDialog.DisplayAlertAsync("You've been succesfully registered",
                    "Please check your email to verify your account, you will now be redirected to the login page", "ok");
                await _navigationService.GoBackAsync();
            }
            else 
            {
                // TODO: Specify directly what are the errors ocurred during signup
                Errors = "There was an error on your submission.";
            }
        }
    }
}
