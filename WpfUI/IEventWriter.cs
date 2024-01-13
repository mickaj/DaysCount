using WpfUI.Models;

namespace WpfUI
{
    public interface IEventWriter
    {
        void Save(Event @event, string filePath);
    }
}
