using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dockpad.Models
{
    public class Mood : INotifyPropertyChanged
    {
        public string Owner { get; set; }

        public string mood { get; set; }

        public string Description { get; set; }

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
