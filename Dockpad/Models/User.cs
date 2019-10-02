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

        public string regex { get; set; }

        public string PhoneNumber { get; set; }

        public object Picture { get; set; }

        public string address { get; set; }

        public string posicion { get; set; }
        public string company{get; set;}

        public string biography{get; set;}

        public string BirthDate { get; set; }

    }
}
