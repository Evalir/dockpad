using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace Dockpad.Models
{
    public class Mood
    {

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("mood")]
        public string mood { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("hightlights")]
        public string Hightlights { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

    }
}
