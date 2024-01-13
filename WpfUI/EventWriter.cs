using System;
using System.IO;
using WpfUI.Models;

namespace WpfUI
{
    public class EventWriter : EventHandlerBase, IEventWriter
    {
        public void Save(Event @event, string filePath)
        {
            if (!File.Exists(filePath)) { File.Delete(filePath); }
            File.WriteAllText(filePath, BuildXmlString(@event));
        }

        private string BuildXmlString(Event @event)
        {
            return String.Format(XmlString, @event.EventDate.ToString(DateFormat), @event.EventName);
        }
    }
}
