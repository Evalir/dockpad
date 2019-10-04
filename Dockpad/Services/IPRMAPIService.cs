﻿using Dockpad.Forms;
using Dockpad.Helpers;
using Dockpad.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dockpad.Services
{
    interface IPRMAPIService
    {
        [Post("/users/signup/")]
        Task<User> SignUp([Body] SignUpForm data);

        [Post("/users/login/")]
        Task<User> Login([Body] LoginForm data);

        [Get("/users/profile/")]
        Task<User> GetProfile([Header("Authorization")] string token);

        [Get("/events/")]
        Task<PaginatedResponse<Event>> GetAllEvents([Header("Authorization")] string token);
    }
}
    