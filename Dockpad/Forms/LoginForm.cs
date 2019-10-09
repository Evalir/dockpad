using Newtonsoft.Json;

namespace Dockpad.Forms
{
    public class LoginForm
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
