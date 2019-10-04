using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Models
{
    public class Contact
    {
        public string Owner { get; set; }

        public string Code { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Nickname { get; set; }

        public string PhoneRegex { get; set; }

        public DateTime Met { get; set; }

        public string FoodPreferences { get; set; }

        public string Pets { get; set; }
    }
}
