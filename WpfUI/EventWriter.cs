using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUI.Models;

namespace WpfUI
{
    public class EventWriter : EventHandlerBase, IEventWriter
    {
        public void Save(IEvent @event, string filePath)
        {
            if (!File.Exists(filePath)) { File.Delete(filePath); }
            File.WriteAllText(filePath, BuildXmlString(@event));
        }

        private string BuildXmlString(IEvent @event)
        {
            return String.Format(XmlString, @event.EventDate.ToString(DateFormat), @event.EventName);
        }
    }
}
