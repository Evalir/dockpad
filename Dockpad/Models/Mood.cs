using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace Dockpad.Models
{
    public class Mood : INotifyPropertyChanged
    {

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("mood")]
        public string mood { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        public Mood()
        {

        }

        public Mood(string Owner, string mood, string Description)
        {
            this.Owner = Owner;
            this.mood = mood;
            this.Description = Description;
            Date = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
