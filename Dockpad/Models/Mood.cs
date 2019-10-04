using System;
using System.Collections.Generic;
using System.Text;

namespace Dockpad.Models
{
    public class Mood
    {
        public string Owner;

        public string mood;

        public string Description;

        public DateTime Date;

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
    }
}
