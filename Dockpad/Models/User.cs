using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Models
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Profile Profile { get; set; }
        public string Token { get; set; }

    }

    public class Profile
    {
        // Returns an URL to the hosted picture
        public string Picture { get; set; }
        public string Address { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Biography { get; set; }

    }
}
