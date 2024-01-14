using System.Collections.Generic;
using WpfUI.Models;

namespace WpfUI
{
    public interface IEventReader
    {
        IEnumerable<Event> Read(string filePath, out string jsonContent);

        Event GetTodayEvent(string todayString);
    }
}
