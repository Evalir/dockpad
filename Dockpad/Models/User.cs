using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Models
{
    public class User
    {
        public string Username { get; set; }

        public DateTime DateJoined { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public object Picture { get; set; }

        public string Address { get; set; }

        public string Company{get; set;}

        public string biography{get; set;}

        public string BirthDate { get; set; }

        public string Token { get; set; }

    }
}
