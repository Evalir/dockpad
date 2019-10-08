using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Forms
{
    public class LoginForm
    {
        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }
    }
}
