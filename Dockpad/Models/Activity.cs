using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Dockpad.Models
{
    public class Activity
    {
        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("lastTime")]
        public DateTime LastTime { get; set; }

        [JsonProperty("partners")]
        public List<string> Partners { get; set; }
    }
}
