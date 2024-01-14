using System;

namespace WpfUI.Models
{
    public class Event
    {
        public DateTime EventDate { get; set; }

        public string EventName { get; set; }

        public Event()
        {
        }

        public Event(DateTime eventDate, string eventName)
        {
            EventDate = eventDate;
            EventName = eventName;
        }
    }
}
