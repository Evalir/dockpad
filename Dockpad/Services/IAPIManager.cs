using Dockpad.Forms;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dockpad.Services
{
    public interface IAPIManager
    {
        Task<HttpResponseMessage> Login([Body] LoginForm data);
        Task<HttpResponseMessage> GetProfile([Header("Authorization")] string token);
        Task<HttpResponseMessage> SignUp([Body] SignUpForm data);
        Task<HttpResponseMessage> GetAllEvents([Header("Authorization")] string token);
    }
}
