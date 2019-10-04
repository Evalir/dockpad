using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Models
{
    public class Activity
    {
        public string Owner { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastTime { get; set; }

        public List<string> Partners { get; set; }
    }
}
