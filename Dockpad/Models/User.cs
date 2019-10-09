using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace Dockpad.Models
{
    public class User 
    {
        [JsonProperty("userName")]
        public string Username { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

    }

    public class Profile 
    {
        // Returns an URL to the hosted picture

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("biography")]
        public string Biography { get; set; }

    }
}
