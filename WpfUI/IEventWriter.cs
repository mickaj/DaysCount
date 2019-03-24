using WpfUI.Models;

namespace WpfUI
{
    public interface IEventWriter
    {
        void Save(IEvent @event, string filePath);
    }
}
