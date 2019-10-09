using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Dockpad
{
    public static class Config
    {
        public static string DomainURL = "https://dockpad.xyz/";
        public static string Token { get; set; }
    }

    public static class EndPoints
    {
        public static string Register = $"{Config.DomainURL}/users/register";

        public static string Login = $"{Config.DomainURL}/users/login";
    }
}
