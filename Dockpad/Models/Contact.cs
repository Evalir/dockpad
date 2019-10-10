using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Dockpad.Models
{
    public class Contact
    {
        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("birthDate")]
        public string BirthDate { get; set; }

        [JsonProperty("nickName")]
        public string Nickname { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("Met")]
        public string Met { get; set; }

        [JsonProperty("foodPreferences")]
        public string FoodPreferences { get; set; }

        [JsonProperty("pets")]
        public string Pets { get; set; }

    }
}
