﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dockpad.Forms;
using Dockpad.Helpers;
using Dockpad.Models;
using Refit;

namespace Dockpad.Services
{
    public class PRMAPIService
    {
        private IPRMAPIService APIResolver {get; set;}

        public PRMAPIService()
        {
            APIResolver = RestService.For<IPRMAPIService>(Config.DomainURL); 
        }

        public async Task<Response<User>> Login(LoginForm form)
        {
            Response<User> response = new Response<User>();
            try
            {
                response.Data = await APIResolver.Login(form);
            } catch (Refit.ApiException ex)
            {
                response.ErrorData = ex;
            }
            return response;
        }

        public async Task<Response<User>> SignUp(SignUpForm form)
        {
            Response<User> response = new Response<User>();
            try
            {
                response.Data = await APIResolver.SignUp(form);
            }
            catch (Refit.ApiException ex)
            {
                response.ErrorData = ex;
            }
            return response;
        }

        public async Task<Response<User>> GetProfile()
        {
            Response<User> response = new Response<User>();
            try
            {
                response.Data = await APIResolver.GetProfile(Config.Token);
            }
            catch (Refit.ApiException ex)
            {
                response.ErrorData = ex;
            }
            return response;
        }
    }
}
