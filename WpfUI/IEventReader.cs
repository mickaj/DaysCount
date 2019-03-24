using WpfUI.Models;

namespace WpfUI
{
    public interface IEventReader
    {
        IEvent Read(string filePath);
        IEvent GetTodayEvent(string todayString);
    }
}
