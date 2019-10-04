using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dockpad.Models
{
    public class Event : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IList<string> Contacts { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
