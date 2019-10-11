using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace Dockpad.Models
{
    public class Meeting
    {
        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("organizer")]
        public string Organizer { get; set; }

        [JsonProperty("contactID")]
        public string ContactID { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }

        [JsonProperty("from")]
        public DateTime From { get; set; }

        [JsonProperty("to")]
        public DateTime To { get; set; }

        [JsonProperty("color")]
        public Color SelectedColor { get; set; }

        [JsonProperty("allDay")]
        public bool AllDay { get; set; }
    }
}
