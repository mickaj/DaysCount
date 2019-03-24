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
    internal static class EventWriter
    {
        internal static string DateFormat = "dd-MM-yyyy";
        internal static CultureInfo CultureInfo = CultureInfo.InvariantCulture;

        internal static string XmlString = "<?xml version=\u00221.0\u0022 encoding=\u0022utf-8\u0022 ?>"
            + "<Days>\n"
            + "<Date>{0}</Date>\n"
            + "<Event>{1}</Event>\n"
            + "</Days>";

        internal static void Save(IEvent @event, string filePath)
        {
            if (!File.Exists(filePath)) { File.Delete(filePath); }
            File.WriteAllText(filePath, BuildXmlString(@event));
        }

        private static string BuildXmlString(IEvent @event)
        {
            return String.Format(XmlString, @event.EventDate.ToString(DateFormat), @event.EventName);
        }

    }
}
