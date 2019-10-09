using Newtonsoft.Json;

namespace Dockpad.Forms
{
    public class SignUpForm
    {
        [JsonProperty("firstName")]
        public string FirstName {get; set;}
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("passwordConfirmation")]
        public string PasswordConfirmation { get; set; }
    }
}
