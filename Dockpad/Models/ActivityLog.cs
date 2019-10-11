using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Dockpad.Models
{
    public class ActivityLog
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("activity")]
        public string Activity { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
