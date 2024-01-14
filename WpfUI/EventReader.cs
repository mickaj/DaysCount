using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using WpfUI.Helpers;
using WpfUI.Models;

namespace WpfUI
{
    public class EventReader : EventHandlerBase, IEventReader
    {
        public IEnumerable<Event> Read(string filePath, out string jsonContent)
        {
            if (!File.Exists(filePath)) { throw new FileNotFoundException(FILE_DOES_NOT_EXIST_EXCEPTION_MESSAGE); }
            else
            {
                jsonContent = GetContentString(filePath);
                return Convert(jsonContent);
            }
        }

        public Event GetTodayEvent(string todayString)
        {
            return new Event(DateTimeHelper.GetNowDateOnly(), todayString);
        }

        private IEnumerable<Event> Convert(string jsonString)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Event>>(jsonString);
        }

        private string GetContentString(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                };
            }
        }
    }
}
