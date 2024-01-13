using WpfUI.Models;

namespace WpfUI
{
    public interface IEventReader
    {
        Event Read(string filePath);
        Event GetTodayEvent(string todayString);
    }
}
