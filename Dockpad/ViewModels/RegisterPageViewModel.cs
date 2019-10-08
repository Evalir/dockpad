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

        private IAPIManager _apiManager;

        public RegisterPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IAPIManager apiManager) : base(navigationService)
        {
            API = new PRMAPI();
            Form = new SignUpForm();
            _apiManager = apiManager;
            _pageDialog = pageDialog;
            RegisterCommand = new DelegateCommand(ExecuteRegister);
        }

        private async void ExecuteRegister()
        {
            var response = await _apiManager.SignUp(Form);
            if (response.IsSuccessStatusCode)
            {
                // TODO: Resolve where the user data will be handled or saved
                await _pageDialog.DisplayAlertAsync("You've been succesfully registered",
                    "Please check your email to verify your account, you will now be redirected to the login page", "ok");
            }
            else 
            {
                var logres = await response.Content.ReadAsStringAsync();
                // Wrong username/password
                Errors = "Incorrect username or password";
            }
        }
    }
}
