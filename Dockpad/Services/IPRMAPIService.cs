using Dockpad.Forms;
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
        Task<User> SignUp([Body] User user);

        [Post("/users/login/")]
        Task<User> Login([Body] LoginForm data);
    }
}
