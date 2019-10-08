using Dockpad.Forms;
using Dockpad.Helpers;
using Dockpad.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dockpad.Services
{
    public interface IPRMAPI
    {
        [Post("/users/signup/")]
        Task<HttpResponseMessage> SignUp([Body] SignUpForm data);

        [Post("/users/login/")]
        Task<HttpResponseMessage> Login([Body] LoginForm data);

        [Get("/users/profile/")]
        Task<HttpResponseMessage> GetProfile([Header("Authorization")] string token);

        [Get("/events/")]
        Task<HttpResponseMessage> GetAllEvents([Header("Authorization")] string token);
    }
}
