using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Forms
{
    public class SignUpForm
    {
        public string firstName {get; set;}
        public string lastName { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        //  TODO: Remove required field for birth date (will be done on backend)
        public string birthDate { get; set; } = "2019-01-01";
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string passwordConfirmation { get; set; }
    }
}
