using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Models
{
    public class ActivityLog
    {
        public string Code { get; set; }

        public string Activity { get; set; }

        public string Owner { get; set; }

        public List<string> companions { get; set; }

        public string Details { get; set; }

        public string Location { get; set; }
    }
}
