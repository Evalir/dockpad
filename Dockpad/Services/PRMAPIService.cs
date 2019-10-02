using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dockpad.Forms;
using Dockpad.Models;
using Refit;

namespace Dockpad.Services
{
    public class PRMAPIService
    {
        public async Task<User> Login(LoginForm form)
        {
            try
            {
                var apiResponse = RestService.For<IPRMAPIService>(Config.DomainURL);
                User login = await apiResponse.Login(form);
                return login;
            } catch (Refit.ApiException ex)
            {
                // if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return null;
                // throw new Refit.ApiException();
            }
        }

        public async void SignUp(User user)
        {
            // var apiResponse = RestService.For<IPRMAPIService>(Config.DomainURL);
            // User signup = await apiResponse.SignUp(user);

        }
    }
}
