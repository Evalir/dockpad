using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Models
{
    public class Activity
    {
        public string Owner;

        public string Code;

        public string Name;

        public string Description;

        public bool IsActive;

        public DateTime LastTime;

        public List<string> Partners;
    }
}
