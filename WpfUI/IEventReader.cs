using System.Collections.Generic;
using WpfUI.Models;

namespace WpfUI
{
    public interface IEventReader
    {
        IEnumerable<Event> Read(string filePath);

        Event GetTodayEvent(string todayString);
    }
}
